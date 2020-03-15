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
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            Settings.Default.PropertyName = textBoxPropertyName.Text;
            Settings.Default.PropertyNamePlainText = textBoxPropertyNamePlainText.Text;

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
    }
}
