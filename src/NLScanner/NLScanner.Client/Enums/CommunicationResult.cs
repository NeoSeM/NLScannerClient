using System;
using System.Collections.Generic;
using System.Text;

namespace NLScanner.Client
{
    public enum T_CommunicationResult
    {
        SendError = 0,  ///< Sending error.
        Support,        ///< Commands supported.
        Unsupport,      ///< Commands not supported. 
        OutOfRange,     ///< Data value is not within the range. 
        UnknownResult,  ///< Unknown error.
    };
}
