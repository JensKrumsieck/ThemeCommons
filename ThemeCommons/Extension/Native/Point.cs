using System.Runtime.InteropServices;

// ReSharper disable once InconsistentNaming

namespace ThemeCommons.Extension.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        public int x;
        public int y;
        public POINT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
