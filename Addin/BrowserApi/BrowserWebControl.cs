using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Net;

namespace VisioHtmlSidebar
{
    class BrowserWebControl : IBrowserApi
    {
        public Control Create()
        {
            Registry.SetValue(
                @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
                "Visio.exe", 11000, RegistryValueKind.DWord);

            Registry.SetValue(
                @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_96DPI_PIXEL",
                "Visio.exe", 1, RegistryValueKind.DWord);

            var editor = new WebBrowser
            {
                Dock = DockStyle.Fill,
                IsWebBrowserContextMenuEnabled = true,
                Location = new Point(0, 0),
                MinimumSize = new Size(20, 20),
                Name = "editor",
                ScriptErrorsSuppressed = true,
                ScrollBarsEnabled = false,
                Size = new Size(832, 638),
                TabIndex = 2,
            };

            return editor;
        }

        public Task ConfigureEnvironment(Control control)
        {
            return Task.FromResult(true);
        }

        public void Navigate(Control control, string url, Action done, Action<string, CookieContainer> navigated)
        {
            var webBrowserControl = control as WebBrowser;
            webBrowserControl.DocumentCompleted += (sender, args) =>
            {
                if (args.Url.ToString() == new Uri(url).ToString())
                {
                    done();
                }
            };
            webBrowserControl.Navigate(url);
        }

        public Task<string> ExecuteScript(Control editor, string name, string val)
        {
            var webBrowserControl = editor as WebBrowser;
            var doc = webBrowserControl.Document;
            var response = doc != null 
                ? doc.InvokeScript(name, new[] { val }) as string
                : string.Empty;

            return Task.FromResult(response);
        }
    }
}
