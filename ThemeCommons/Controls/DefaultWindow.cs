using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using ThemeCommons.Extension.Native;

namespace ThemeCommons.Controls
{
    public class DefaultWindow : Window
    {
        static DefaultWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DefaultWindow), new FrameworkPropertyMetadata(typeof(DefaultWindow)));
        }

        public FrameworkElement TitlebarContent
        {
            get => (FrameworkElement)GetValue(TitlebarContentProperty);
            set => SetValue(TitlebarContentProperty, value);
        }
        public static readonly DependencyProperty TitlebarContentProperty = DependencyProperty.Register("TitlebarContent", typeof(FrameworkElement), typeof(DefaultWindow));

        public Brush TitlebarBackground
        {
            get => (Brush)GetValue(TitlebarBackgroundProperty);
            set => SetValue(TitlebarBackgroundProperty, value);
        }
        public static readonly DependencyProperty TitlebarBackgroundProperty = DependencyProperty.Register("TitlebarBackground", typeof(Brush), typeof(DefaultWindow));

        public double TitlebarHeight
        {
            get => (double)GetValue(TitlebarHeightProperty);
            set => SetValue(TitlebarHeightProperty, value);
        }
        public static readonly DependencyProperty TitlebarHeightProperty = DependencyProperty.Register("TitlebarHeight", typeof(double), typeof(DefaultWindow));

        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }
        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof(double), typeof(DefaultWindow), new PropertyMetadata(SystemParameters.SmallIconHeight));

        public bool ShowIcon
        {
            get => (bool)GetValue(ShowIconProperty);
            set => SetValue(ShowIconProperty, value);
        }
        public static readonly DependencyProperty ShowIconProperty = DependencyProperty.Register("ShowIcon", typeof(bool), typeof(DefaultWindow), new PropertyMetadata(true));

        public DefaultWindow()
        {
            SourceInitialized += Window_SourceInitialized;
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var handle = new WindowInteropHelper(this).Handle;
            var handleSource = HwndSource.FromHwnd(handle);
            handleSource?.AddHook(WindowProc);
            NativeMethods.EnableBlur(handle);
        }

        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    NativeMethods.WMGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
                case 0x0046:
                    var pos = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));
                    var src = PresentationSource.FromVisual(this);
                    if ((pos.flags & 0x0002) != 0) return IntPtr.Zero;
                    var wnd = (Window)HwndSource.FromHwnd(hwnd)?.RootVisual;
                    if (wnd == null || src == null) return IntPtr.Zero;

                    var dpiX = src.CompositionTarget?.TransformToDevice.M11 ?? 1;
                    var dpiY = src.CompositionTarget?.TransformToDevice.M22 ?? 1;

                    var changed = false;
                    if (pos.cx / dpiX < MinWidth) { pos.cx = (int)(MinWidth * dpiX); changed = true; }
                    if (pos.cy / dpiY < MinHeight) { pos.cy = (int)(MinHeight * dpiY); changed = true; }
                    if (!changed) return IntPtr.Zero;

                    Marshal.StructureToPtr(pos, lParam, true);
                    handled = true;
                    break;
            }
            return IntPtr.Zero;
        }
    }
}
