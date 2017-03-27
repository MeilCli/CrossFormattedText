using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossFormattedText.Abstractions {

    [Flags]
    public enum FontAttributes {
        None = 1,
        Bold = 1 << 1,
        Italic = 1 << 2
    }
}
