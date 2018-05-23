using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HtmlFormData.Properties;
using Microsoft.Office.Core;
using Visio = Microsoft.Office.Interop.Visio;

namespace HtmlFormData
{
    public partial class TheForm : Form
    {
        private int _currentShapeId;
        private bool _needUpdate;
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

            // webBrowser.ObjectForScripting = this;
            
            webBrowser.Navigate(new Uri(GetResourcePath(@"edit.html")));
            webBrowser.DocumentCompleted += WebBrowserOnDocumentCompleted;
            _window.SelectionChanged += WindowOnSelectionChanged;
            _window.Application.VisioIsIdle += ApplicationVisioIsIdle;

            Closed += OnClosed;
        }

        private void ApplicationVisioIsIdle(Visio.Application app)
        {
            if (_needUpdate)
            {
                try
                {
                    UpdateSidebar();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                _needUpdate = false;
            }
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            _window.SelectionChanged -= WindowOnSelectionChanged;
            _window.Application.VisioIsIdle -= ApplicationVisioIsIdle;
        }

        private void WebBrowserOnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs webBrowserDocumentCompletedEventArgs)
        {
            _needUpdate = true;
            
        }

        private void WindowOnSelectionChanged(Visio.Window window)
        {
            _needUpdate = true;
        }

        void UpdateSidebar()
        {
            var document = webBrowser.Document;

            if (document == null)
                return;

            if (_currentShapeId != 0)
            {
                var currentShape = _window.PageAsObj.Shapes.ItemFromID[_currentShapeId];
                if (currentShape != null)
                {
                    var oldTargetCell = GetTargetCell(currentShape);
                    if (oldTargetCell != null)
                    {
                        var newText = document.InvokeScript("getText").ToString();
                        var oldText = GetCellText(oldTargetCell);
                        if (newText != oldText)
                            SetCellText(oldTargetCell, newText);
                    }
                }
            }

            var newCurrentShape = _window.Selection.PrimaryItem;
            _currentShapeId = newCurrentShape != null ? newCurrentShape.ID : 0;
            var newTargetCell = newCurrentShape != null ? GetTargetCell(newCurrentShape) : null;
            if (newTargetCell != null)
            {
                var comment = GetCellText(newTargetCell);
                document.InvokeScript("setText", new object [] { comment });
            }
            else
            {
                document.InvokeScript("disableText");
            }
        }

        private Visio.Cell GetTargetCell(Visio.Shape shape)
        {
            var propName = Settings.Default.PropertyName;
            return shape.CellExistsU[propName, 0] != 0 ? shape.CellsU[propName] : null;
        }

        private void SetCellText(Visio.Cell cell, string text)
        {
            if (cell != null)
                cell.FormulaU = text != null ? "\"" + text.Replace("\"", "\"\"") + "\"" : "No Formula";
        }

        private string GetCellText(Visio.Cell cell)
        {
            return cell.ResultStrU[0];
        }
    }
}
