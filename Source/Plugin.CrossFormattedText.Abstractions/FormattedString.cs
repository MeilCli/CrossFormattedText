using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossFormattedText.Abstractions {

    public class FormattedString : IEnumerable<Span>{

        public static readonly FormattedString Empty = new FormattedString();

        private Span[] spans;

        public FormattedString() {
            spans = new Span[0];
        }

        public FormattedString(Span[] spans) {
            this.spans = spans;
        }

        public int Length {
            get {
                int size = 0;
                foreach(var span in spans) {
                    size += span.Text.Length;
                }
                return size;
            }
        }

        internal CharSpan[] ToCharSpanArray() {
            var ar = new CharSpan[Length];
            int index = 0;
            for(int i = 0;i < spans.Length;i++) {
                Span span = spans[i];
                foreach(var c in span.Text) {
                    ar[index] = new CharSpan(c,i);
                    index++;
                }
            }
            return ar;
        }

        public bool Contains(string value) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }
            if(value.Length == 0) {
                return true;
            }
           
            char[] vAr = value.ToCharArray();
            CharSpan[] sAr = ToCharSpanArray();

            if(vAr.Length == 1) {
                foreach(var c in sAr) {
                    if(c.Character == vAr[0]) {
                        return true;
                    }
                }
                return false;
            }

            // vAr.Length >= 1
            for(int i = 0;i < sAr.Length;i++) {
                if(sAr[i].Character != vAr[0]) {
                    continue;
                }
                for(int j = 1;j < vAr.Length && i + j < sAr.Length;j++) {
                    if(sAr[i + j].Character != vAr[j]) {
                        break;
                    }
                    if(j == vAr.Length - 1) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool Contains(Span value) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            foreach(var span in spans) {
                if(span.Equals(value)) {
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<Span> GetEnumerator() {
            foreach(var span in spans) {
                yield return span;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
