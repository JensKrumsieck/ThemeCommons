using System.Windows;
using System.Windows.Media;

namespace ThemeCommons.Extension
{
    public static class ControlExtension
    {
        /// <summary>
        /// Attached SpecialColorProperty
        /// </summary>
        public static readonly DependencyProperty SpecialColorProperty =
            DependencyProperty.RegisterAttached("SpecialColor", typeof(Color), typeof(ControlExtension),
                new PropertyMetadata(Colors.OrangeRed));
        public static void SetSpecialColor(DependencyObject obj, Color value) => 
            obj.SetValue(SpecialColorProperty, value);
        public static Color GetSpecialColor(DependencyObject obj) => 
            (Color)obj.GetValue(SpecialColorProperty);
    }
}
