using System.Windows;
using System.Windows.Controls;

namespace ThemeCommons.Controls
{
    public class Popup : ContentControl
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(string), typeof(Popup));
        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        static Popup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Popup),
                new FrameworkPropertyMetadata(typeof(Popup)));
        }
    }
}