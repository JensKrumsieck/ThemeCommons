using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleInvalidCastException
// ReSharper disable IdentifierTypo
// ReSharper disable NonReadonlyMemberInGetHashCode
namespace ThemeCommons.Extension.Native
{
    public class NativeMethods
    {
        [DllImport("user32.dll")]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        private const int WM_COPYDATA = 0x004A;

        /// <summary>
        /// Adjusts Size when maximized
        /// See https://stackoverflow.com/questions/6890472/wpf-maximize-window-with-windowstate-problem-application-will-hide-windows-ta
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        public static void WMGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            var mmi = (MINMAXINFO) Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            const int MONITOR_DEFAULTTONEAREST = 0x00000002;
            var monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != IntPtr.Zero)
            {
                var monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                var rcWorkArea = monitorInfo.rcWork;
                var rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }
            Marshal.StructureToPtr(mmi, lParam, true);
        }

        /// <summary>
        /// Native Methods from WinAPI to Send and Get Messages between instances
        /// https://www.codeproject.com/Articles/1224031/Passing-Parameters-to-a-Running-Application-in-WPF
        /// </summary>
        public static string GetMessage(int message, IntPtr lParam)
        {
            if (message != WM_COPYDATA) return null;
            try
            {
                var data = Marshal.PtrToStructure<COPYDATASTRUCT>(lParam);
                var result = (string)data.lpData.Clone();
                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Native Methods from WinAPI to Send and Get Messages between instances
        /// https://www.codeproject.com/Articles/1224031/Passing-Parameters-to-a-Running-Application-in-WPF
        /// </summary>
        public static void SendMessage(IntPtr hwnd, string message)
        {
            var messageBytes = Encoding.Unicode.GetBytes(message);
            var data = new COPYDATASTRUCT
            {
                dwData = IntPtr.Zero,
                lpData = message,
                cbData = messageBytes.Length + 1 /* +1 because of \0 string termination */
            };

            if (SendMessage(hwnd, WM_COPYDATA, IntPtr.Zero, ref data) != 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Enables Blur
        /// https://github.com/riverar/sample-win32-acrylicblur/blob/master/MainWindow.xaml.cs
        /// </summary>
        /// <param name="handle"></param>
        public static void EnableBlur(IntPtr handle)
        {
            var accent = new AccentPolicy
            {
                AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND,
                AccentFlags = 2,
                GradientColor = 0x00FFFFFF
            };
            var accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };
            SetWindowCompositionAttribute(handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
    }
}