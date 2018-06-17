using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using HtmlFormData.Properties;
using Visio = Microsoft.Office.Interop.Visio;

namespace HtmlFormData
{
    public partial class TheForm : Form
    {
        private const short VIS_SECTION_PROP = (short)Visio.VisSectionIndices.visSectionProp;

        private Visio.Shape _editorShape;
        private string _currentShapeHtml;

        private readonly Visio.Window _window;

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

            webBrowser.DocumentCompleted += WebBrowserOnDocumentCompleted;
            _window.SelectionChanged += WindowOnSelectionChanged;

            _editorShape = _window.Selection.PrimaryItem;
            ReloadEditor();
            
            Closed += OnClosed;
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            _window.SelectionChanged -= WindowOnSelectionChanged;
            Win32.DisableClickSounds(false);
        }

        private void WebBrowserOnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs args)
        {
            var document = webBrowser.Document;
            if (document == null)
                return;

            var targetCell = GetTargetCell(_editorShape, Settings.Default.PropertyName);
            if (targetCell != null)
            {
                _currentShapeHtml = GetCellText(targetCell);
                document.InvokeScript("setEditorHtml", new object[] {_currentShapeHtml});
            }
            else
            {
                _currentShapeHtml = string.Empty;
                document.InvokeScript("disableEditor");
            }
        }

        private void WindowOnSelectionChanged(Visio.Window window)
        {
            SaveEditorToShape();

            _editorShape = _window.Selection.PrimaryItem;
            ReloadEditor();
        }

        void SaveEditorToShape()
        {
            if (_editorShape == null)
                return;

            var targetCell = GetTargetCell(_editorShape, Settings.Default.PropertyName);
            if (targetCell == null)
                return;

            var document = webBrowser.Document;
            if (document == null)
                return;

            var newHtml = document.InvokeScript("getEditorHtml").ToString();
            if (newHtml != _currentShapeHtml)
            {
                SetCellText(targetCell, newHtml);

                var targetPlainTextCell = GetTargetCell(_editorShape, Settings.Default.PropertyNamePlainText);
                if (targetPlainTextCell != null)
                {
                    var newText = document.InvokeScript("getEditorText").ToString().Replace("\r\n\r\n", "\r\n");
                    SetCellText(targetPlainTextCell, newText);
                }

                _window.Application.AddUndoUnit(new UndoUnit(ReloadEditor, ReloadEditor));
            }
        }

        private void ReloadEditor()
        {
            webBrowser.Navigate(new Uri(GetResourcePath(@"edit.html")));
        }

        private Visio.Cell GetTargetCell(Visio.Shape shape, string propName)
        {
            if (shape == null)
                return null;

            if (string.IsNullOrEmpty(propName))
                return null;

            if (shape.CellExistsU[propName, 0] != 0)
                return shape.CellsU[propName];

            if (shape.SectionExists[VIS_SECTION_PROP, 0] == 0)
                return null;

            for (short rowIndex = 0; rowIndex < shape.RowCount[VIS_SECTION_PROP]; ++rowIndex)
            {
                if (propName == shape.CellsSRC[VIS_SECTION_PROP, rowIndex, (short) Visio.VisCellIndices.visCustPropsLabel].ResultStr[-1])
                    return shape.CellsSRC[VIS_SECTION_PROP, rowIndex, (short) Visio.VisCellIndices.visCustPropsValue];
            }

            return null;
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
    }
}
