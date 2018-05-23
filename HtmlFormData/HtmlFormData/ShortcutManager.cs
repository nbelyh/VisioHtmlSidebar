using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Visio = Microsoft.Office.Interop.Visio;

namespace HtmlFormData
{
    public class ShortcutManager
    {
        private static HashSet<Keys> KeysFromString(string keys)
        {
            var cult = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            var kc = new KeysConverter();
            var result = new HashSet<Keys>(
                keys
                    .Split(',')
                    .Select(s => s.Trim())
                    .Select(kc.ConvertFromString)
                    .Cast<Keys>()
                    .ToList());

            Thread.CurrentThread.CurrentUICulture = cult;
            return result;
        }

        private static readonly Lazy<HashSet<Keys>> ControlShortcutKeys = new Lazy<HashSet<Keys>>(() =>
            KeysFromString(
                "Tab, Tab+Shift, Tab+Ctrl, Ctrl+Shift+Tab, " +
                "Escape, PgUp, PgDn, End, Home, Ins, " +
                "Del, F3, " +
                "Shift+Ins, Shift+Del, Ctrl+Back, Ctrl+Space, Ctrl+End, " +
                "Ctrl+Home, Ctrl+Ins, Ctrl+Del, Ctrl+A, " +
                "Ctrl+B, Ctrl+C, Ctrl+E, Ctrl+F, Ctrl+G, Ctrl+H, Ctrl+I, Ctrl+M, Ctrl+N, Ctrl+R, Ctrl+Y, Ctrl+U, " +
                "Ctrl+V, Ctrl+X, Ctrl+Z, Alt+F"));

        public bool OnKeystrokeMessageForAddon(Visio.MSGWrap msgWrap)
        {
            var keys = (Keys) msgWrap.wParam;
            if ((Control.ModifierKeys & Keys.Control) != 0)
                keys |= Keys.Control;
            if ((Control.ModifierKeys & Keys.Shift) != 0)
                keys |= Keys.Shift;

            var control = Control.FromChildHandle((IntPtr) msgWrap.hwnd) as WebBrowser;
            if (control == null)
                return false;

            if (ControlShortcutKeys.Value.Contains(keys))
            {
                var msg = new NativeMethods.MSG
                {
                    hwnd = (IntPtr)msgWrap.hwnd,
                    message = (UInt32)msgWrap.message,
                    wParam = (IntPtr)msgWrap.wParam,
                    lParam = (IntPtr)msgWrap.lParam
                };

                var accel = (NativeMethods.IOleInPlaceActiveObject)control.ActiveXInstance;
                accel.TranslateAccelerator(ref msg);

                NativeMethods.TranslateMessage(ref msg);
                NativeMethods.DispatchMessage(ref msg);
                return true;
            }

            return false;
        }
    }
}
