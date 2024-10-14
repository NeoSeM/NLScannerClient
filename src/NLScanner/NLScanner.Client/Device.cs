using NLScanner.Client.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLScanner.Client
{
    public class Device
    {
        public string Info { get; internal set; }
        public int DeviceIndex { get; internal set; }
        public DeviceType DeviceType { get; internal set; }
    }
}
