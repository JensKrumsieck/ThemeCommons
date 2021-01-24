using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using ThemeCommons.Extension;

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

        public DefaultWindow()
        {
            SourceInitialized += new EventHandler(Window_SourceInitialized);
        }

        private void Window_SourceInitialized(object? sender, EventArgs e)
        {
            var handle = new WindowInteropHelper(this).Handle;
            var handleSource = HwndSource.FromHwnd(handle);
            handleSource?.AddHook(WindowProc);
        }

        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg != 0x0024) return IntPtr.Zero;
            NativeMethods.WMGetMinMaxInfo(hwnd, lParam);
            handled = true;
            return IntPtr.Zero;
        }
    }
}
