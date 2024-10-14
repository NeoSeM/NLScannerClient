using NLScanner.Client.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NLScanner.Client
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DeviceInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
        public string devInfo;   // Will store the device info as a string

        public DeviceType devType;   // Assuming this is an enum or int
    }
}
