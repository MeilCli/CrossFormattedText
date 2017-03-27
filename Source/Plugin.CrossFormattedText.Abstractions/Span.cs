using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plugin.CrossFormattedText.Abstractions {

    public class Span {

        public string Text { get; set; } = string.Empty;

        public SpanColor BackgroundColor { get; set; }

        public SpanColor ForegroundColor { get; set; }

        public FontAttributes FontAttributes { get; set; } = FontAttributes.None;

        public FontSize FontSize { get; set; } = FontSize.Normal;

        public ICommand Command { get; set; }

        /// <summary>
        /// If use Command, require this
        /// </summary>
        public object CommandParameter { get; set; }

    }

    public class SpanColor {
        public int Alpha { get; }
        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }

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

    public struct FontSize {

        public static readonly FontSize UltraBig = new FontSize(1.5f);
        public static readonly FontSize Big = new FontSize(1.25f);
        public static readonly FontSize Normal = new FontSize(1f);
        public static readonly FontSize Small = new FontSize(0.75f);
        public static readonly FontSize UltraSmall = new FontSize(0.5f);

        public float Proportion { get; }

        public FontSize(float proportion) {
            Proportion = proportion;
        }
    }
}
