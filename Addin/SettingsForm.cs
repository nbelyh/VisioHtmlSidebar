using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.Windows.Forms;
using VisioHtmlSidebar.Properties;

namespace VisioHtmlSidebar
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            textBoxPropertyName.Text = Settings.Default.PropertyName;
            textBoxPropertyNamePlainText.Text = Settings.Default.PropertyNamePlainText;
            checkBoxReplaceHtmlReports.Checked = Settings.Default.ReplaceDefaultHtmlReport;
            checkBoxEnableWebView2.Checked = Settings.Default.EnableWebView2;
            checkBoxEnableWebView2.Enabled = IsWebView2RuntimeInstalled();

        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            Settings.Default.PropertyName = textBoxPropertyName.Text;
            Settings.Default.PropertyNamePlainText = textBoxPropertyNamePlainText.Text;
            Settings.Default.EnableWebView2 = checkBoxEnableWebView2.Checked;

            if (checkBoxReplaceHtmlReports.Checked != Settings.Default.ReplaceDefaultHtmlReport)
            {
                Settings.Default.ReplaceDefaultHtmlReport = checkBoxReplaceHtmlReports.Checked;

                if (Settings.Default.ReplaceDefaultHtmlReport)
                    RunReportHook.Install();
                else
                    RunReportHook.Uninstall();
            }

            Settings.Default.Save();
        }

        private bool IsWebView2RuntimeInstalled()
        {
            string availableVersion = null;
            try
            {
                availableVersion = CoreWebView2Environment.GetAvailableBrowserVersionString();
            }
            catch (WebView2RuntimeNotFoundException e)
            {
                Trace.TraceWarning($"WebView2 runtime not found: {e}");
            }

            return availableVersion != null && CoreWebView2Environment.CompareBrowserVersions(availableVersion, "100.0.0.0") >= 0;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://go.microsoft.com/fwlink/p/?LinkId=2124703");
        }
    }
}
