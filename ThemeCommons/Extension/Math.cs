namespace ThemeCommons.Extension
{
    public static class MathEx
    {
        /// <summary>
        /// Constraints input to be between min and max
        /// </summary>
        /// <param name="input"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double Clamp(double input, double min, double max) =>
            input < min ? input = min : input > max ? max : input;
    }
}
