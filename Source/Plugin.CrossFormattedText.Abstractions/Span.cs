using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossFormattedText.Abstractions {
    public class Span {

        public string Text { get; set; }

        public SpanColor BackgroundColor { get; set; }

        public SpanColor ForegroundColor { get; set; }

        public FontAttributes FontAttributes { get; set; } = FontAttributes.None;
    }

    public class SpanColor {
        public int Alpha { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public SpanColor(int red,int green,int blue) : this(255,red,green,blue) { }

        public SpanColor(int alpha,int red,int green,int blue) {
            Alpha = alpha;
            Red = red;
            Green = green;
            Blue = blue;
        }
    }

    [Flags]
    public enum FontAttributes {
        None = 1,
        Bold = 1 << 1,
        Italic = 1 << 2
    }
}
