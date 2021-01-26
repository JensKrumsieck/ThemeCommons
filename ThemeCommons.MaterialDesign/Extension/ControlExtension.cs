using MaterialDesignThemes.Wpf;
using System.Windows;

namespace ThemeCommons.MaterialDesign.Extension
{
    public static class ControlExtension
    {
        /// <summary>
        ///Attached Icon Extension
        /// </summary>
        public static readonly DependencyProperty AttachedIconProperty =
            DependencyProperty.RegisterAttached("AttachedIcon", typeof(PackIconKind), typeof(ControlExtension), new PropertyMetadata(PackIconKind.None));

        public static void SetAttachedIcon(UIElement element, PackIconKind value) => element.SetValue(AttachedIconProperty, value);

        public static PackIconKind GetAttachedIcon(UIElement element) => (PackIconKind)element.GetValue(AttachedIconProperty);
    }
}
