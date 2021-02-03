using System;
using System.Windows;
using System.Windows.Controls;

namespace ThemeCommons.Controls
{
    [TemplatePart(Name = "ColorPicker", Type = typeof(ColorPicker))]
    public class ColorPickerTextBox : TextBox
    {
        static ColorPickerTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerTextBox),
                new FrameworkPropertyMetadata(typeof(ColorPickerTextBox)));
        }

        public ColorPicker Picker;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Picker = GetTemplateChild("ColorPicker") as ColorPicker;
            if (Picker != null) Picker.ColorSelected += PickerOnColorSelected;
            TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e) =>
            Picker.SetValue(ColorPicker.SelectedHexStringProperty, Text);

        private void PickerOnColorSelected(object sender, EventArgs e) => SetValue(TextProperty, Picker.SelectedHexString);

    }
}
