using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace ThemeCommons.MarkupExtensions
{
    [MarkupExtensionReturnType(typeof(FontFamily))]
    public class FontExtension : MarkupExtension
    {
        public string FontFamily { get; set; }
        private readonly Lazy<FontFamily> _font;
        public FontExtension()
        {
            _font = new Lazy<FontFamily>(() => new FontFamily(new Uri($"pack://application:,,,/ThemeCommons;component/Assets/{FontFamily}/"), $"./#{FontFamily}"));
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => _font.Value;
    }
}
