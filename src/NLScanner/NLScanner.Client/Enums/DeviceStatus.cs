using System;
using System.Collections.Generic;
using System.Text;

namespace NLScanner.Client
{
    public enum T_DeviceStatus
    {
        Opened = 0,         ///< Opened.
        NotOpened,          ///< Not opened.
        Closed,             ///< Closed.
        NotClosed,          ///< Not closed.
        Updating,           ///< Updating...
        Updated,            ///< Updating is finished.
        Writing,            ///< Writing data...
        Written,            ///< Data writing is finished.
        Reading,            ///< Reading data...
        ReadOK,             ///< Data reading is finished.
        GettingPicData,     ///< Getting image data...
        GetPicDataOK,       ///< Image data has been ontained.
        GettingPicColorType,
        GetPicColorTypeOk,
        ConveringColorSpace,///< Convering image color space
        ConvertColorSpaceOK,///< Conversion finished.
        UnknownStatus       ///< Unknown status.
    };
}
