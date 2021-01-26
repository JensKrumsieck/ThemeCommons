using System;
using System.Windows.Markup;
using System.Windows.Media;
using ThemeCommons.Extension;

namespace ThemeCommons.MarkupExtensions
{
    [MarkupExtensionReturnType(typeof(Color))]
    public class LightenExtension : MarkupExtension
    {
        public Color Source { get; set; }
        public double Amount { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider) => Source.ToHSVColor().Lighten(Amount + 1).ToMediaColor();
    }
}
