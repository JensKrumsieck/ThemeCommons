using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ThemeCommons.Converter
{
    public class AddValueConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (double)value + System.Convert.ToDouble(parameter);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
