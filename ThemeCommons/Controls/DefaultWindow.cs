using System.Windows;

namespace ThemeCommons.Controls
{
    public class DefaultWindow : Window
    {
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
    }
}
