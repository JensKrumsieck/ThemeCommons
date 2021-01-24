using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Math;

namespace ThemeCommons.Controls
{
    /// <summary>
    /// Inspired by         https://docs.microsoft.com/en-us/archive/blogs/mim/winrt-create-a-custom-itemspanel-for-an-itemscontrol
    /// and                 https://github.com/sh-akira/ColorPickerWPF/blob/master/akr.WPF.Controls.ColorPicker/CirclePanel.cs
    /// </summary>
    public class CirclePanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            if (InternalChildren.Count == 0) return availableSize;

            //Cast to use LINQ
            var children = InternalChildren.Cast<UIElement>().ToArray();
            foreach (var child in children) child.Measure(availableSize);

            var maxWidth = children.Max(s => s.DesiredSize.Width);
            var maxHeight = children.Max(s => s.DesiredSize.Height);

            var r = (maxWidth * children.Length) / PI * 2 + Max(maxWidth, maxHeight);

            var result = new Size(r * 2, r * 2);

            if (!double.IsInfinity(availableSize.Width) && availableSize.Width < result.Width)
                result.Width = availableSize.Width;
            if (!double.IsInfinity(availableSize.Height) && availableSize.Height < result.Height)
                result.Height = availableSize.Height;

            return result;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (InternalChildren.Count == 0) return finalSize;

            var firstRect = finalSize.Width > finalSize.Height
                ? new Rect((finalSize.Width - finalSize.Height) / 2, 0, finalSize.Height, finalSize.Height)
                : new Rect(0, (finalSize.Height - finalSize.Width) / 2, finalSize.Width, finalSize.Width);

            var stepDegrees = 360.0 / InternalChildren.Count;

            for (var i = 0; i < InternalChildren.Count; i++)
            {
                var child = InternalChildren[i];
                var location = new Point(firstRect.Left + (firstRect.Width - child.DesiredSize.Width) / 2, firstRect.Top);
                child.RenderTransform = new RotateTransform(stepDegrees * i, child.DesiredSize.Width / 2, finalSize.Height / 2 - firstRect.Top);
                child.Arrange(new Rect(location, child.DesiredSize));
            }

            return finalSize;
        }
    }
}
