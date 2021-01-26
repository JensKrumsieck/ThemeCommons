using System;
using ThemeCommons.Primitives;

namespace ThemeCommons.Extension
{
    public static class ColorUtil
    {
        /// <summary>
        /// Darkens Color by Amount
        /// </summary>
        /// <param name="input"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static HSVColor Darken(this HSVColor input, double amount) => new HSVColor(input.A, input.H, input.S, Math.Pow(input.V, amount));

        /// <summary>
        /// Lightens Color by Amount
        /// </summary>
        /// <param name="input"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static HSVColor Lighten(this HSVColor input, double amount) => new HSVColor(input.A, input.H, input.S, Math.Pow(input.V, 1/amount));
    }
}
