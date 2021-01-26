using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using ThemeCommons.Extension;

namespace ThemeCommons.Converter
{
    [ValueConversion(typeof(SolidColorBrush), typeof(SolidColorBrush))]
    public class Lighten : MarkupExtension, IValueConverter
    {
        public double Amount { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var src = (SolidColorBrush)value;
            if (src == null) return null;
            return new SolidColorBrush(src.Color.ToHSVColor().Lighten(Amount + 1).ToMediaColor())
            {
                Opacity = src.Opacity
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
