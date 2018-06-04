using System.Windows.Forms;
using HtmlFormData.Properties;

namespace HtmlFormData
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            textBoxPropertyName.Text = Settings.Default.PropertyName;
            textBoxPropertyNamePlainText.Text = Settings.Default.PropertyNamePlainText;
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            Settings.Default.PropertyName = textBoxPropertyName.Text;
            Settings.Default.PropertyNamePlainText = textBoxPropertyNamePlainText.Text;
            Settings.Default.Save();
        }
    }
}
