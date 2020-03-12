using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Visio = Microsoft.Office.Interop.Visio;

namespace HtmlFormData
{
    public static class ControlId
    {
        public const int ID_BUTTON_OK = 1;
        public const int ID_BUTTON_CANCEL = 2;

        public const int ID_BUTTON_RUN = 0x0406;

        public const int ID_EDIT_RPT_FILE_PATH = 0x36EC;
        public static int ID_LISTVIEW_REPORT_DEFINITIONS_LIST = 0x36B3;
    }

    public static class RunReportHook
    {
        private static int _hHook;
        private static ReportsDialog _dlgReports;
        private static RunReportDialog _dlgRunReport;

        private static readonly Win32.WindowsHookProc HookProc = HookCallback;

        public static string RptDefName { get;set; }
        public static string RptOutputFileName { get; set; }

        public static void Install()
        {
            var app = Globals.ThisAddIn.Application;
            app.EnterScope += ApplicationOnEnterScope;
            app.ExitScope += ApplicationOnExitScope;
        }

        public static void Uninstall()
        {
            var app = Globals.ThisAddIn.Application;
            app.EnterScope -= ApplicationOnEnterScope;
            app.ExitScope -= ApplicationOnExitScope;
        }

        private static void Hook()
        {
            Unhook();
            RptDefName = null;
            RptOutputFileName = null;
            _hHook = Win32.SetWindowsHookEx(Win32.WH_CBT, HookProc, IntPtr.Zero, Win32.GetCurrentThreadId()); 
        }

        private static void Unhook()
        {
            if (_dlgRunReport != null)
            {
                _dlgRunReport.ReleaseHandle();
                _dlgRunReport = null;
            }

            if (_dlgReports != null)
            {
                _dlgReports.ReleaseHandle();
                _dlgReports = null;
            }

            if (_hHook != 0)
            {
                Win32.UnhookWindowsHookEx(_hHook);
                _hHook = 0;
            }
        }

        private static int HookCallback(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code == Win32.HCBT_ACTIVATE)
            {
                if (TestForDialog(wParam, "Run Report"))
                {
                    _dlgRunReport = new RunReportDialog();
                    _dlgRunReport.AssignHandle(wParam);
                }

                if (TestForDialog(wParam, "Reports"))
                {
                    _dlgReports = new ReportsDialog();
                    _dlgReports.AssignHandle(wParam);
                }
            }
            return Win32.CallNextHookEx(_hHook, code, wParam, lParam); 
        }

        private static bool TestForDialog(IntPtr hWnd, string targetTitle)
        {
            var cls = Win32.GetClassName(hWnd);
            if (cls == "#32770") // MessageBoxes are Dialog boxes
            {
                var title = Win32.GetWindowText(hWnd);
                if (title == targetTitle)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsReportScope(string bstrDescription)
        {
            return bstrDescription.Contains("Report") && bstrDescription != "Report;0";
        }

        private static void ApplicationOnEnterScope(Visio.Application app, int nScopeId, string bstrDescription)
        {
            if (IsReportScope(bstrDescription))
            {
                Hook();
            }
        }

        private static void ApplicationOnExitScope(Visio.Application app, int nScopeId, string bstrDescription, bool bErrOrCancelled)
        {
            if (IsReportScope(bstrDescription))
            {
                Unhook();

                var rptDefName = RptDefName;
                if (string.IsNullOrEmpty(rptDefName))
                    return;

                var rptOutputFilename = RptOutputFileName;
                if (string.IsNullOrEmpty(rptOutputFilename))
                    return;

                var tempFileName = $"{Path.GetTempPath()}{Guid.NewGuid():D}.html";
                app.Addons["VisRpt"].Run($"/rptDefName={rptDefName} /rptOutput=HTML /rptSilent=1 /rptOutputFilename={tempFileName}");

                var src = File.ReadAllText(tempFileName);

                var dst = PostFormatHtmlReport.PostProcess(src);

                File.WriteAllText(rptOutputFilename, dst);
                Process.Start(rptOutputFilename);
            }
        }
    }

    public class RunReportDialog : NativeWindow
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Win32.WM_COMMAND)
            {
                var code = (int) m.WParam >> 16;
                var id = (int) m.WParam & 0xFFFF;

                if (code == Win32.BN_CLICKED && id == ControlId.ID_BUTTON_OK)
                {
                    var hwndEdit = Win32.GetDlgItem(Handle, ControlId.ID_EDIT_RPT_FILE_PATH);
                    RunReportHook.RptOutputFileName = Win32.GetWindowText(hwndEdit);

                    var hwndCancel = Win32.GetDlgItem(Handle, ControlId.ID_BUTTON_CANCEL);
                    Win32.PostMessage(hwndCancel, Win32.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
                    return;
                }
            }

            base.WndProc(ref m);
        }
    }

    public class ReportsDialog : NativeWindow
    {
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.WM_NOTIFY:
                {
                    var notify = (Win32.NMHDR)Marshal.PtrToStructure(m.LParam, typeof(Win32.NMHDR));
                    if (notify.code == Win32.NM_DBLCLK) {
                        SaveReportName();
                    }
                    break;
                }

                case Win32.WM_COMMAND:
                {
                    var code = (int) m.WParam >> 16;
                    var controlId = (int) m.WParam & 0xFFFF;

                    if (code == Win32.BN_CLICKED && 
                        (controlId == ControlId.ID_BUTTON_RUN || controlId == ControlId.ID_BUTTON_OK))
                    {
                        SaveReportName();
                    }

                    break;
                }
            }
        
            base.WndProc(ref m);
        }

        private void SaveReportName()
        {
            var hwndList = Win32.GetDlgItem(Handle, ControlId.ID_LISTVIEW_REPORT_DEFINITIONS_LIST);
            var item = GetSelecteItem(hwndList);

            var name = GetItemText(hwndList, item, 0);
            var path = GetItemText(hwndList, item, 1);

            RunReportHook.RptDefName = path.EndsWith(".vrd", StringComparison.OrdinalIgnoreCase)
                ? Path.GetFileName(path)
                : name;
        }

        private string GetItemText(IntPtr hwndList, int item, int subitem)
        {
            // Declare and populate the LVITEM structure
            var lvi = new Win32.LVITEM
            {
                mask = Win32.LVIF_TEXT,
                cchTextMax = 512,
                iItem = item,
                iSubItem = subitem,
                pszText = Marshal.AllocHGlobal(512)
            };

            var ptrLvi = Marshal.AllocHGlobal(Marshal.SizeOf(lvi));
            Marshal.StructureToPtr(lvi, ptrLvi, false);
            try
            {
                Win32.SendMessage(hwndList, Win32.LVM_GETITEMW, IntPtr.Zero, ptrLvi);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            // Extract the text of the specified item
            string itemText = Marshal.PtrToStringAuto(lvi.pszText);
            return itemText;
        }

        private int GetSelecteItem(IntPtr hwndList)
        {
            return Win32.SendMessage(hwndList, Win32.LVM_GETNEXTITEM, (IntPtr)(-1), (IntPtr)Win32.LVNI_SELECTED);
        }
    }
}