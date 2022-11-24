using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;
using VisioHtmlSidebar.Properties;
using Visio = Microsoft.Office.Interop.Visio;

namespace VisioHtmlSidebar
{
    public partial class ThisAddIn
    {
        private readonly AddinUI AddinUI = new AddinUI();
        private readonly ShortcutManager _shortcutManager = new ShortcutManager();

        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return AddinUI;
        }

        /// <summary>
        /// A command to demonstrate conditionally enabling/disabling.
        /// The command gets enabled only when a shape is selected
        /// </summary>
        public void OpenSettings()
        {
            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        void SetIsEditing(bool set)
        {
            Settings.Default.EditMode= set;
        }

        /// <summary>
        /// Callback called by the UI manager when user clicks a button
        /// Should do something meaningful when corresponding action is called.
        /// </summary>
        public void OnCommand(string commandId)
        {
            switch (commandId)
            {
                case "OpenSettings":
                    OpenSettings();
                    return;

                case "ToggleEdit":
                    SetIsEditing(!Settings.Default.EditMode);
                    return;

                case "TogglePanel":
                    TogglePanel();
                    return;
            }
        }

        /// <summary>
        /// Callback called by UI manager.
        /// Should return if corresponding command should be enabled in the user interface.
        /// By default, all commands are enabled.
        /// </summary>
        public bool IsCommandEnabled(string commandId)
        {
            switch (commandId)
            {
                case "OpenSettings":    // make command2 always enabled
                    return true;

                case "ToggleEdit":
                    return IsPanelEnabled();

                case "TogglePanel": // make panel enabled only if we have selected shape(s).
                    return IsPanelEnabled();

                default:
                    return true;
            }
        }

        /// <summary>
        /// Callback called by UI manager.
        /// Should return if corresponding command (button) is pressed or not (makes sense for toggle buttons)
        /// </summary>
        public bool IsCommandChecked(string command)
        {

            if (command == "TogglePanel")
                return IsPanelVisible();

            if (command == "ToggleEdit")
                return Settings.Default.EditMode;

            return false;
        }
        /// <summary>
        /// Callback called by UI manager.
        /// Returns a label associated with given command.
        /// We assume for simplicity taht command labels are named simply named as [commandId]_Label (see resources)
        /// </summary>
        public string GetCommandLabel(string command)
        {
            return Resources.ResourceManager.GetString(command + "_Label");
        }

        /// <summary>
        /// Returns a bitmap associated with given command.
        /// We assume for simplicity that bitmap ids are named after command id.
        /// </summary>
        public Bitmap GetCommandBitmap(string id)
        {
            return (Bitmap)Resources.ResourceManager.GetObject(id);
        }

        public void TogglePanel()
        {
            _panelManager.TogglePanel(Application.ActiveWindow);
        }

        public bool IsPanelEnabled()
        {
            return Application != null && Application.ActiveWindow != null;
        }

        public bool IsPanelVisible()
        {
            return Application != null
                && _panelManager != null
                && _panelManager.IsPanelOpened(Application.ActiveWindow);
        }

        private PanelManager _panelManager;

        internal void UpdateUI()
        {
            AddinUI.UpdateRibbon();
        }

        private void Application_SelectionChanged(Visio.Window window)
        {
            UpdateUI();
        }

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.EnableVisualStyles();

            try
            {
                Settings.Default.Reload();
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Unable to load addin settings {exception.Message}");
            }

            if (Settings.Default.ReplaceDefaultHtmlReport)
                RunReportHook.Install();

            _panelManager = new PanelManager(this);
            var version = int.Parse(Application.Version, NumberStyles.AllowDecimalPoint);
            Application.SelectionChanged += Application_SelectionChanged;
            Application.OnKeystrokeMessageForAddon += _shortcutManager.OnKeystrokeMessageForAddon;
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            if (Settings.Default.ReplaceDefaultHtmlReport)
                RunReportHook.Uninstall();

            _panelManager.Dispose();
            Application.SelectionChanged -= Application_SelectionChanged;
            Application.OnKeystrokeMessageForAddon -= _shortcutManager.OnKeystrokeMessageForAddon;
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            Startup += ThisAddIn_Startup;
            Shutdown += ThisAddIn_Shutdown;
        }

    }
}
