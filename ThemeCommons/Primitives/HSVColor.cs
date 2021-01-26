using ThemeCommons.Extension;

namespace ThemeCommons.Primitives
{
    public struct HSVColor
    {
        public double A { get;  }
        public double H { get;  }
        public double S { get;  }
        public double V { get;  }

        public HSVColor(double a, double h, double s, double v)
        {
            A = a;
            H = h;
            S = s;
            V = v;
        }
        /// <summary>
        /// Conversion with outside alpha parameter
        /// </summary>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public string ToHexString() => ColorConverter.ColorFromAHSV(A, H, S, V).ToString();
    }
}
