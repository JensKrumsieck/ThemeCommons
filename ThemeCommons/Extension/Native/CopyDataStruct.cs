using System;
using System.Runtime.InteropServices;

// ReSharper disable once InconsistentNaming

namespace ThemeCommons.Extension.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpData;
    }
}
