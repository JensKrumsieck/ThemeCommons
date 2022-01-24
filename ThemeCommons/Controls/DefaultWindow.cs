using System;
using System.Windows;
using System.Windows.Media;

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
            StateChanged += MaximizeFix;
        }

        private void MaximizeFix(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                BorderThickness = new Thickness(8);
            else BorderThickness = new Thickness(0);
        }
    }
}
