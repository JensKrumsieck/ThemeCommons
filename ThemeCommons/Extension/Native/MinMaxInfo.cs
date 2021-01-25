using System.Runtime.InteropServices;

// ReSharper disable once InconsistentNaming
// ReSharper disable IdentifierTypo

namespace ThemeCommons.Extension.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MINMAXINFO
    {
        public POINT ptReserved;
        public POINT ptMaxSize;
        public POINT ptMaxPosition;
        public POINT ptMinTrackSize;
        public POINT ptMaxTrackSize;
    }
}
