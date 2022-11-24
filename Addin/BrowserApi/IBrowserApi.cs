using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net;

namespace VisioHtmlSidebar
{
    interface IBrowserApi
    {
        Control Create();
        Task ConfigureEnvironment(Control editor);
        void Navigate(Control control, string url, Action done, Action<string, CookieContainer> navigated);
        Task<string> ExecuteScript(Control control, string name, string args);
    }
}
