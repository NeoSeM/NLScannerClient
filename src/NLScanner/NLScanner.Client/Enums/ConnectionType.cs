using System;
using System.Collections.Generic;
using System.Text;

namespace NLScanner.Client
{
    public enum ConnectionType
    {
        Serial = 1,
        USB = 2,
        NetDev = 4,
        All = Serial | USB | NetDev
    }
}
