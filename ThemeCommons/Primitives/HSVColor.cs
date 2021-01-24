using ThemeCommons.Extension;

namespace ThemeCommons.Primitives
{
    public record HSVColor(double a, double h, double s, double v)
    {
        /// <summary>
        /// Conversion with outside alpha parameter
        /// </summary>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public string ToHexString() => ColorUtil.ColorFromAHSV(a, h, s, v).ToString();
    }
}
