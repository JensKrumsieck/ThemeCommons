using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ThemeCommons.Controls
{
    public class MouseTrackerDecorator : Decorator
    {
        private static readonly DependencyProperty MousePositionProperty = DependencyProperty.Register("MousePosition", typeof(Point), typeof(MouseTrackerDecorator));
        private static readonly DependencyProperty RelativeMousePositionProperty = DependencyProperty.Register("RelativeMousePosition", typeof(Point), typeof(MouseTrackerDecorator));

        public override UIElement Child
        {
            get => base.Child;
            set
            {
                if (base.Child != null)
                    base.Child.MouseMove -= _controlledObject_MouseMove;
                base.Child = value;
                base.Child.MouseMove += _controlledObject_MouseMove;
            }
        }

        public Point MousePosition
        {
            get => (Point)GetValue(MousePositionProperty);
            set => SetValue(MousePositionProperty, value);
        }

        public Point RelativeMousePosition
        {
            get => (Point)GetValue(RelativeMousePositionProperty);
            set => SetValue(RelativeMousePositionProperty, value);
        }

        private void _controlledObject_MouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(base.Child);
            MousePosition = p;
            RelativeMousePosition = new Point(MousePosition.X / Child.RenderSize.Width, MousePosition.Y / Child.RenderSize.Height);
        }
    }
}