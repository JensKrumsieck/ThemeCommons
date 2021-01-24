using System;
using System.Net;
using ThemeCommons.Primitives;
using Color = System.Windows.Media.Color;
using DrawingColor = System.Drawing.Color;

namespace ThemeCommons.Extension
{
    public static class ColorUtil
    {
        /// <summary>
        /// Calculates Color from HSV
        /// see https://de.wikipedia.org/wiki/HSV-Farbraum#Umrechnung_HSV_in_RGB
        /// </summary>
        /// <param name="h"></param>
        /// <param name="s"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static (byte r, byte g, byte b) RGBFromHSV(double h, double s, double v)
        {
            h = Math.Abs(h - 360) < 1e-9 ? 0 : h / 60;
            var i = (int)Math.Truncate(h);
            var f = h - i;

            var p = v * (1d - s);
            var q = v * (1d - s * f);
            var t = v * (1d - s * (1 - f));

            var (r, g, b) = i switch
            {
                1 => (q, v, p),
                2 => (p, v, t),
                3 => (p, q, v),
                4 => (t, p, v),
                5 => (v, p, q),
                _ => (v, t, p)
            };
            return ((byte)Math.Round(r * 0xFF), (byte)Math.Round(g * 0xFF), (byte)Math.Round(b * 0xFF));
        }

        /// <summary>
        /// Converts RGB To HSV
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static (double h, double s, double v) HSVFromRGB(byte r, byte g, byte b)
        {
            var max = Math.Max(r, Math.Max(g, b));
            var min = Math.Min(r, Math.Min(g, b));
            var hue = DrawingColor.FromArgb(0xFF, r, g, b).GetHue();
            var saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            var value = max / 255d;
            return (hue, saturation, value);
        }

        /// <summary>
        /// Converts AHSV Values To <see cref="System.Windows.Media.Color"/>
        /// </summary>
        /// <param name="a">alpha value in 0-1</param>
        /// <param name="h"></param>
        /// <param name="s"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Color ColorFromAHSV(double a, double h, double s, double v)
        {
            var (r, g, b) = RGBFromHSV(h, s, v);
            return Color.FromArgb((byte)Math.Round(a * 0xFF), r, g, b);
        }

        /// <summary>
        /// Converts ARGB Values to <see cref="ThemeCommons.Primitives.HSVColor"/>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static HSVColor HSVColorFromARGB(byte a, byte r, byte g, byte b)
        {
            var (h, s, v) = HSVFromRGB(r, g, b);
            return new HSVColor(a / 255d, h, s, v);
        }

        /// <summary>
        /// Converts HSVColor to <see cref="System.Windows.Media.Color"/>
        /// </summary>
        /// <param name="hsv"></param>
        /// <returns></returns>
        public static Color ToMediaColor(this HSVColor hsv) => ColorFromAHSV(hsv.a, hsv.h, hsv.s, hsv.v);

        public static HSVColor ToHSVColor(this Color color) => HSVColorFromARGB(color.A, color.R, color.G, color.B);

        //Internal Conversion
        private static Color FromDrawingColor(DrawingColor col) => Color.FromArgb(col.A, col.R, col.G, col.B);
    }
}
