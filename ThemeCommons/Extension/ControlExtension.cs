using System.Windows;
using System.Windows.Controls;
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
                new PropertyMetadata(Colors.Transparent));
        public static void SetSpecialColor(DependencyObject obj, Color value) =>
            obj.SetValue(SpecialColorProperty, value);
        public static Color GetSpecialColor(DependencyObject obj) =>
            (Color)obj.GetValue(SpecialColorProperty);

        /// <summary>
        /// Attached SpecialBrushProperty
        /// </summary>
        public static readonly DependencyProperty SpecialBrushProperty =
            DependencyProperty.RegisterAttached("SpecialBrush", typeof(SolidColorBrush), typeof(ControlExtension));
        public static void SetSpecialBrush(DependencyObject obj, SolidColorBrush value) =>
            obj.SetValue(SpecialBrushProperty, value);
        public static SolidColorBrush GetSpecialBrush(DependencyObject obj) =>
            (SolidColorBrush)obj.GetValue(SpecialBrushProperty);

        /// <summary>
        /// Attached Stackpanel Orientaton
        /// </summary>
        public static readonly DependencyProperty StackPanelOrientationProperty =
            DependencyProperty.RegisterAttached("StackPanelOrientation", typeof(Orientation), typeof(ControlExtension), new FrameworkPropertyMetadata(Orientation.Horizontal));
        public static void SetStackPanelOrientation(DependencyObject obj, Orientation value) =>
            obj.SetValue(StackPanelOrientationProperty, value);
        public static Orientation StackPanelOrientation(DependencyObject obj) =>
            (Orientation)obj.GetValue(StackPanelOrientationProperty);
    }
}
