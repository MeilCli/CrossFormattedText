using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossFormattedText.Abstractions {

    public struct CharSpan {

        public char Character { get; }
        public Span Span { get; }
        internal int SpanNumber { get; }

        internal CharSpan(char character,int spanNumber) : this(character,null,spanNumber) { }

        internal CharSpan(char character,Span span) : this(character,span,-1) { }

        internal CharSpan(char character,Span span,int spanNumber) {
            Character = character;
            Span = span;
            SpanNumber = spanNumber;
        }
    }
}
