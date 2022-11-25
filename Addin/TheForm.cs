using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using AngleSharp.Parser.Html;
using Newtonsoft.Json;
using VisioHtmlSidebar.Properties;
using Visio = Microsoft.Office.Interop.Visio;

namespace VisioHtmlSidebar
{
    public partial class TheForm : Form
    {
        private const short visSectionUser = (short)Visio.VisSectionIndices.visSectionUser;

        private Visio.Shape _editorShape;
        private string _currentShapeHtml;

        private readonly Visio.Window _window;

        private Control browserControl;
        private IBrowserApi browserApi;

        static string GetResourcePath(string fileName)
        {
            var assemblyPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var assemblyDirectory = Path.GetDirectoryName(assemblyPath);
            return Path.Combine(assemblyDirectory ?? string.Empty, "Resources", fileName);
        }

        /// <summary>
        /// Form constructor, receives parent Visio diagram window
        /// </summary>
        /// <param name="window">Parent Visio diagram window</param>
        public TheForm(Visio.Window window)
        {
            _window = window;
            InitializeComponent();

            Win32.DisableClickSounds(true);

            _window.SelectionChanged += WindowOnSelectionChanged;
            _editorShape = _window.Selection.PrimaryItem;

            Closed += OnClosed;

            if (Settings.Default.EnableWebView2)
                browserApi = new BrowserWebView2();
            else
                browserApi = new BrowserWebControl();

            Settings.Default.PropertyChanged += Default_PropertyChanged;
        }

        private void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ReloadEditor();
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            _window.SelectionChanged -= WindowOnSelectionChanged;
            Win32.DisableClickSounds(false);
        }

        Visio.Shape TargetShape => 
            _editorShape ?? _window.PageAsObj.PageSheet;

        private void WindowOnSelectionChanged(Visio.Window window)
        {
            SaveEditorToShape();

            _editorShape = _window.Selection.PrimaryItem;
            ReloadEditor();
        }

        async public void SaveEditorToShape()
        {
            if (TargetShape == null)
                return;

            var targetCell = GetTargetCell(TargetShape, Settings.Default.PropertyName);
            if (targetCell == null)
                return;

            var newHtml = await browserApi.ExecuteScript(this.browserControl, "getEditorHtml", null);
            if (newHtml != _currentShapeHtml)
            {
                SetCellText(targetCell, newHtml);

                var targetPlainTextCell = GetTargetCell(TargetShape, Settings.Default.PropertyNamePlainText);
                if (targetPlainTextCell != null)
                {
                    var parser = new HtmlParser();
                    var doc = parser.Parse(newHtml);

                    var newText = doc.TextContent.Replace("\r\n\r\n", "\r\n");
                    SetCellText(targetPlainTextCell, newText);
                }

                _window.Application.AddUndoUnit(new UndoUnit(ReloadEditor, ReloadEditor));
            }
        }

        private async void ReloadEditor()
        {
            var targetCell = GetTargetCell(TargetShape, Settings.Default.PropertyName);
            _currentShapeHtml = (targetCell != null) ? GetCellText(targetCell) : string.Empty;
            await browserApi.ExecuteScript(browserControl, "setEnabled", Settings.Default.EditMode ? "true" : null);
            await browserApi.ExecuteScript(browserControl, "setEditorHtml", _currentShapeHtml);
        }

        private Visio.Cell GetTargetCell(Visio.Shape shape, string cellName)
        {
            if (shape == null)
                return null;

            if (string.IsNullOrEmpty(cellName))
                return null;

            if (shape.CellExistsU[cellName, 0] != 0)
                return shape.CellsU[cellName];

            if (!cellName.StartsWith("User."))
                return null;

            if (shape.SectionExists[visSectionUser, 0] == 0)
                shape.AddSection(visSectionUser);

            var cellLocalName = cellName.Split('.')[1];
            short row = shape.AddNamedRow(visSectionUser, cellLocalName, 0);

            var result = shape.CellsSRC[visSectionUser, row, (short)Visio.VisCellIndices.visUserValue];

            result.FormulaU = "No Formula";

            return result;
        }

        private void SetCellText(Visio.Cell cell, string text)
        {
            if (cell != null)
                cell.FormulaU = text != null ? "\"" + text.Replace("\"", "\"\"") + "\"" : "No Formula";
        }

        private string GetCellText(Visio.Cell cell)
        {
            return string.IsNullOrEmpty(cell.FormulaU) 
                ? string.Empty
                : cell.ResultStrU[(short)Visio.VisUnitCodes.visUnitsString];
        }

        private void TheForm_Load(object sender, EventArgs e)
        {
            InitBrowser();
        }

        private async void InitBrowser()
        {
            this.browserControl = browserApi.Create();

            await browserApi.ConfigureEnvironment(browserControl);

            Controls.Add(browserControl);

            browserApi.Navigate(browserControl, GetResourcePath(@"edit.html"), () => ReloadEditor(), (target, cookies) => { });
        }
    }
}
