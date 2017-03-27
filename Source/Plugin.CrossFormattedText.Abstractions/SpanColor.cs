using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossFormattedText.Abstractions {

    public struct SpanColor : IEquatable<SpanColor> {

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
}
