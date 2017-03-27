using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plugin.CrossFormattedText.Abstractions {

    public class Span {

        public string Text { get; set; } = string.Empty;

        public SpanColor BackgroundColor { get; set; } = SpanColor.DefaultValue;

        public SpanColor ForegroundColor { get; set; } = SpanColor.DefaultValue;

        public FontAttributes FontAttributes { get; set; } = FontAttributes.None;

        public FontSize FontSize { get; set; } = FontSize.Normal;

        public ICommand Command { get; set; }

        /// <summary>
        /// If use Command, require this
        /// </summary>
        public object CommandParameter { get; set; }

    }

    public struct SpanColor : IEquatable<SpanColor>{

        public static readonly SpanColor DefaultValue = new SpanColor(-1,-1,-1,-1);

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

        public override bool Equals(object obj) {
            if(obj is SpanColor) {
                return Equals((SpanColor)obj);
            }
            return false;
        }

        public bool Equals(SpanColor color) {
            return Alpha == color.Alpha && Red == color.Red && Green == color.Green && Blue == color.Blue;
        }

        public override int GetHashCode() {
            return Alpha ^ Red ^ Green ^ Blue;
        }

        public static bool operator ==(SpanColor a,SpanColor b) {
            return a.Equals(b);
        }

        public static bool operator !=(SpanColor a,SpanColor b) {
            return (a == b) == false;
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
