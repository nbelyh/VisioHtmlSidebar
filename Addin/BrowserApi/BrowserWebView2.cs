using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.Net;
using System.Web;

namespace VisioHtmlSidebar
{
    class BrowserWebView2 : IBrowserApi
    {
        public static CoreWebView2Environment EnvironmentWebView2;

        public Control Create()
        {
            var editor = new WebView2
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                MinimumSize = new Size(20, 20),
                Name = "editor",
                Size = new Size(832, 638),
                TabIndex = 2,
            };

            return editor;
        }

        public async Task<CookieContainer> GetCookieContainer(Control control, string url)
        {
            var webView2 = control as WebView2;
            var mgr = webView2.CoreWebView2.CookieManager;
            var cookies = await mgr.GetCookiesAsync(url);

            var cc = new CookieContainer();
            cookies.ForEach(c =>
            {
                cc.Add(new Cookie(c.Name, c.Value, c.Path, c.Domain));
            });


            return cc;
        }

        public async Task ConfigureEnvironment(Control control)
        {
            var webView2 = control as WebView2;

            if (EnvironmentWebView2 == null)
            {
                var userDataFolder = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "SvgPublishWebView2");
                EnvironmentWebView2 = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
            }

            await webView2.EnsureCoreWebView2Async(EnvironmentWebView2);
        }

        public void Navigate(Control control, string url, Action done, Action<string, CookieContainer> navigated)
        {
            var webView2 = control as WebView2;
            webView2.Source = new Uri(url);
            webView2.CoreWebView2.DOMContentLoaded += (e, a) =>
            {
                done();
            };
            webView2.NavigationCompleted += async (e, a) =>
            {
                navigated(webView2.Source.ToString(), await GetCookieContainer(webView2, url));
            };
        }

        public Task<string> ExecuteScript(Control editor, string name, string val)
        {
            var webView2 = editor as WebView2;
            var param = HttpUtility.JavaScriptStringEncode(val, true);
            return webView2.ExecuteScriptAsync($"{name}({param})");
        }
    }
}
