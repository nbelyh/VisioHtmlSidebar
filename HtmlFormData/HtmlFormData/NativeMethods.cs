using System;
using System.Runtime.InteropServices;
using System.Security;

namespace HtmlFormData
{
    public static class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public IntPtr hwnd;
            public UInt32 message;
            public IntPtr wParam;
            public IntPtr lParam;
            public UInt32 time;
            public POINT pt;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class COMRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32", EntryPoint = "DispatchMessage")]
        public static extern IntPtr DispatchMessage(ref MSG lpMsg);

        [DllImport("user32", EntryPoint = "TranslateMessage")]
        public static extern bool TranslateMessage(ref MSG lpMsg);

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [SuppressUnmanagedCodeSecurity]
        [Guid("00000117-0000-0000-C000-000000000046")]
        public interface IOleInPlaceActiveObject
        {
            [PreserveSig]
            int GetWindow(out IntPtr hwnd);
            void ContextSensitiveHelp(int fEnterMode);

            [PreserveSig]
            int TranslateAccelerator([In] ref NativeMethods.MSG lpmsg);

            void OnFrameWindowActivate(bool fActivate);

            void OnDocWindowActivate(int fActivate);

            void ResizeBorder(
                [In] NativeMethods.COMRECT prcBorder,
                [In] NativeMethods.IOleInPlaceUIWindow pUIWindow,
                bool fFrameWindow);

            void EnableModeless(int fEnable);
        }

        [ComImport]
        [Guid("00000115-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleInPlaceUIWindow
        {
            IntPtr GetWindow();

            [PreserveSig]
            int ContextSensitiveHelp(int fEnterMode);

            [PreserveSig]
            int GetBorder([Out] NativeMethods.COMRECT lprectBorder);

            [PreserveSig]
            int RequestBorderSpace([In] NativeMethods.COMRECT pborderwidths);

            [PreserveSig]
            int SetBorderSpace([In] NativeMethods.COMRECT pborderwidths);

            void SetActiveObject(
                [In, MarshalAs(UnmanagedType.Interface)] IOleInPlaceActiveObject pActiveObject,
                [In, MarshalAs(UnmanagedType.LPWStr)] string pszObjName);
        }


        const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        const int SET_FEATURE_ON_PROCESS = 0x00000002;

        [DllImport("urlmon.dll")]
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        static extern int CoInternetSetFeatureEnabled(int FeatureEntry,
            [MarshalAs(UnmanagedType.U4)] int dwFlags,
            bool fEnable);

        public static void DisableClickSounds(bool disable)
        {
            CoInternetSetFeatureEnabled(FEATURE_DISABLE_NAVIGATION_SOUNDS, SET_FEATURE_ON_PROCESS, disable);
        }
    }
}