using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossFormattedText.Abstractions {

    public struct CharSpan {

        public char Character { get; }

        /// <summary>
        /// The span reference containing this character
        /// </summary>
        public Span Span { get; }

        internal CharSpan(char character,Span span) {
            Character = character;
            Span = span;
        }
    }
}
