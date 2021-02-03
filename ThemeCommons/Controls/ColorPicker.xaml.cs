using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ThemeCommons.Extension;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;
using Point = System.Windows.Point;
using ViewColor = ThemeCommons.Primitives.Color;

namespace ThemeCommons.Controls
{
    /// <summary>
    /// Colorpicker control
    /// Inspired by https://github.com/sh-akira/ColorPickerWPF (MIT License)
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(ColorPicker));
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public static readonly DependencyProperty SelectedBrushProperty = DependencyProperty.Register(nameof(SelectedBrush), typeof(SolidColorBrush), typeof(ColorPicker));
        public SolidColorBrush SelectedBrush
        {
            get => (SolidColorBrush)GetValue(SelectedBrushProperty);
            set => SetValue(SelectedBrushProperty, value);
        }

        public static readonly DependencyProperty SelectedHexStringProperty = DependencyProperty.Register(
            nameof(SelectedHexString), typeof(string), typeof(ColorPicker),
            new FrameworkPropertyMetadata((o, args) =>
            {
                var me = o as ColorPicker;
                me?.UpdateColor(me.SelectedHexString);
            }));
        public string SelectedHexString
        {
            get => (string)GetValue(SelectedHexStringProperty);
            set => SetValue(SelectedHexStringProperty, value);
        }

        private readonly ViewColor _selectedColor = new ViewColor();
        private Point? _selectedColorShadingPosition;
        private readonly TranslateTransform _shadingSelectorTransform = new TranslateTransform();
        private readonly TranslateTransform _aSelectorTransform = new TranslateTransform();

        public ColorPicker()
        {
            InitializeComponent();
            const byte alpha = 0xFF;

            var colors = new List<Color>();
            for (byte j = 128; j < 0xFF; j++) colors.Add(Color.FromArgb(alpha, j, 0xFF, 0x00));
            for (byte j = 0x00; j < 0xFF; j++) colors.Add(Color.FromArgb(alpha, 0xFF, (byte)(0xFF - j), 0x00));
            for (byte j = 0x00; j < 0xFF; j++) colors.Add(Color.FromArgb(alpha, 0xFF, 0x00, j));
            for (byte j = 0x00; j < 0xFF; j++) colors.Add(Color.FromArgb(alpha, (byte)(0xFF - j), 0x00, 0xFF));
            for (byte j = 0x00; j < 0xFF; j++) colors.Add(Color.FromArgb(alpha, 0, j, 0xFF));
            for (byte j = 0x00; j < 0xFF; j++) colors.Add(Color.FromArgb(alpha, 0, 0xFF, (byte)(0xFF - j)));
            for (byte j = 0x00; j < 127; j++) colors.Add(Color.FromArgb(alpha, j, 0xFF, 0x00));
            var labels = colors.Select(color => new Border { Height = 22, Width = 2, Background = new SolidColorBrush(color), BorderThickness = new Thickness(0) }).ToList();
            foreach (var label in labels)
                CirclePanel.Children.Add(label);
            DataContext = this;
            ShadingRect.DataContext = _selectedColor;
            ATextBox.DataContext = _selectedColor;
            ABorder.DataContext = _selectedColor;
            ShadingSelector.RenderTransform = _shadingSelectorTransform;
            ASelector.RenderTransform = _aSelectorTransform;
        }

        private void UpdateCircleColorSelector(Point p)
        {
            var r = CircleCanvas.ActualWidth / 2d;
            var rad = Math.Atan2(p.X - r, p.Y - r);
            SetCircleSelectorPosition(rad);

            var hue = (rad * 180 / Math.PI) + 180 - 270;
            if (hue < 0) hue += 360;
            if (hue > 360) hue -= 360;
            _selectedColor.H = hue;
        }

        private void UpdateCircleSelectorPosition(double hue)
        {
            var radian = (hue - 180 + 270) * Math.PI / 180;
            if (hue < 0) hue += 360;
            if (hue > 360) hue -= 360;
            SetCircleSelectorPosition(radian);
        }

        private void SetCircleSelectorPosition(double radian)
        {
            var r = CircleCanvas.ActualWidth / 2d;
            var hcw = CircleSelector.ActualWidth / 2d;
            var hch = CircleSelector.ActualHeight / 2d;
            var newX = Math.Sin(radian) * (r - hcw) + r;
            var newY = Math.Cos(radian) * (r - hch) + r;
            Canvas.SetLeft(CircleSelector, newX - hcw);
            Canvas.SetTop(CircleSelector, newY - hch);
        }

        private void UpdateRectangleColorSelector(Point p)
        {
            //Constraints Point to be between zero and width/height
            p.Y = MathEx.Clamp(p.Y, 0, RectangleCanvas.ActualHeight);
            p.X = MathEx.Clamp(p.X, 0, RectangleCanvas.ActualWidth);
            _shadingSelectorTransform.X = p.X - ShadingSelector.Width / 2;
            _shadingSelectorTransform.Y = p.Y - ShadingSelector.Height / 2;

            p.X /= RectangleCanvas.ActualWidth;
            p.Y /= RectangleCanvas.ActualWidth;
            _selectedColorShadingPosition = p;

            _selectedColor.S = p.X;
            _selectedColor.V = 1 - p.Y;
        }

        private void UpdateASliderSelector(Point p)
        {
            var maxWidth = ACanvas.ActualWidth - ASelector.Width;
            p.Y = 0;
            p.X = MathEx.Clamp(p.X, 0, maxWidth);

            _aSelectorTransform.X = p.X;
            _aSelectorTransform.Y = p.Y;

            p.X /= maxWidth;

            _selectedColor.A = p.X;
        }

        private void UpdateASliderSelectorPosition()
        {
            var maxWidth = ACanvas.ActualWidth - ASelector.Width;
            _aSelectorTransform.X = maxWidth * _selectedColor.A;
            _aSelectorTransform.Y = 0;
        }

        /// <summary>
        /// Sets Slider Positions when selected color updates
        /// </summary>
        private void UpdatePositions()
        {
            //_selectedColor has Updated
            UpdateCircleSelectorPosition(_selectedColor.H);

            var p = new Point(_selectedColor.S, 1 - _selectedColor.V);

            _selectedColorShadingPosition = p;

            _shadingSelectorTransform.X = (p.X * RectangleCanvas.Width) - (ShadingSelector.Width / 2);
            _shadingSelectorTransform.Y = (p.Y * RectangleCanvas.Height) - (ShadingSelector.Height / 2);
        }

        /// <summary>
        /// Updates color when string changes
        /// </summary>
        private void UpdateColor(string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            //text is to short and does not contain #
            if (text.Length <= 6 && !text.Contains("#")) text = "#" + text;
            //text is RGB but not ARGB
            if (text.Length == 7) text = text.Replace("#", "#FF");
            //Now should be ARGB, if not -> go away, it can also be three int shorthand color
            if (text.Length != 9 && text.Length != 4) return;
            var color = (Color)ColorConverter.ConvertFromString(text);
            var hsv = color.ToHSVColor();
            _selectedColor.HSVColor = hsv;
            UpdatePositions();
            OnColorSelected();
        }

        #region Circle Canvas Events
        private void CircleCanvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(CircleCanvas);
            UpdateCircleColorSelector(p);
            CircleCanvas.CaptureMouse();
            e.Handled = true;
        }

        private void CircleCanvas_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) => CircleCanvas.ReleaseMouseCapture();

        private void CircleCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            var canvas = sender as Canvas;
            var p = e.GetPosition(canvas);
            UpdateCircleColorSelector(p);
            Mouse.Synchronize();
        }

        private void CircleCanvas_OnSizeChanged(object sender, SizeChangedEventArgs e) =>
            UpdateCircleSelectorPosition(_selectedColor.H);
        #endregion

        #region Rectangle Canvas Events
        private void RectangleCanvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(RectangleCanvas);
            UpdateRectangleColorSelector(p);
            RectangleCanvas.CaptureMouse();
            e.Handled = true;
        }

        private void RectangleCanvas_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) =>
            RectangleCanvas.ReleaseMouseCapture();

        private void RectangleCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            var p = e.GetPosition(RectangleCanvas);
            UpdateRectangleColorSelector(p);
            Mouse.Synchronize();
        }

        private void RectangleCanvas_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_selectedColorShadingPosition == null) return;
            var p = new Point
            {
                X = _selectedColorShadingPosition.Value.X * e.NewSize.Width,
                Y = _selectedColorShadingPosition.Value.Y * e.NewSize.Height
            };
            UpdateRectangleColorSelector(p);
        }
        #endregion

        #region Hex TextBox Events
        private void HexTextBox_Update(object sender, RoutedEventArgs e) => UpdateColor(HexTextBox.Text);

        private void HexTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) UpdateColor(HexTextBox.Text);
        }
        #endregion

        #region ColorPicker Events
        private void ColorPicker_OnLoaded(object sender, RoutedEventArgs e)
        {
            _selectedColor.PropertyChanged += (s, args) =>
            {
                SelectedColor = _selectedColor.HSVColor.ToMediaColor();
                SelectedBrush = _selectedColor.Brush;
                SelectedHexString = _selectedColor.HexString;
            };
            if (string.IsNullOrEmpty(HexTextBox.Text)) HexTextBox.Text = "ff0000";
            UpdateColor(HexTextBox.Text);
        }

        public event EventHandler ColorSelected;

        protected virtual void OnColorSelected() => ColorSelected?.Invoke(this, EventArgs.Empty);

        #endregion

        #region ASlider Events
        private void ACanvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(ACanvas);
            UpdateASliderSelector(p);
            ACanvas.CaptureMouse();
            e.Handled = true;
        }

        private void ACanvas_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) =>
            ACanvas.ReleaseMouseCapture();

        private void ACanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            var p = e.GetPosition(ACanvas);
            UpdateASliderSelector(p);
            Mouse.Synchronize();
        }

        private void ACanvas_OnSizeChanged(object sender, SizeChangedEventArgs e) => UpdateASliderSelectorPosition();

        #endregion

       
    }
}
