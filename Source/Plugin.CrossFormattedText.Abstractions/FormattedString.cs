using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossFormattedText.Abstractions {

    public class FormattedString : IEnumerable<Span> {

        public static readonly FormattedString Empty = new FormattedString();

        private Span[] spans;

        public FormattedString() {
            spans = new Span[0];
        }

        public FormattedString(Span[] spans) {
            this.spans = spans;
        }

        public Span this[int index] {
            get {
                if(index < 0 || index >= spans.Length) {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return spans[index].Clone();
            }
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

            if(vAr.Length > sAr.Length) {
                return false;
            }

            // vAr.Length >= 1 && vAr.Length <= sAr.Length
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

        public bool EndsWith(string value) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }
            if(value.Length == 0) {
                return true;
            }

            char[] vAr = value.ToCharArray();
            CharSpan[] sAr = ToCharSpanArray();

            if(vAr.Length > sAr.Length) {
                return false;
            }

            for(int i = vAr.Length - 1, j = sAr.Length - 1;i >= 0;i--, j--) {
                if(vAr[i] != sAr[j].Character) {
                    return false;
                }
            }
            return true;
        }

        public bool EndsWith(Span value) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }
            if(spans.Length == 0) {
                return false;
            }

            return spans[spans.Length - 1].Equals(value);
        }

        public int IndexOf(char value) {
            return IndexOf(value,0);
        }

        public int IndexOf(char value,int startIndex) {
            return IndexOf(value,startIndex,Length - startIndex);
        }

        public int IndexOf(char value,int startIndex,int count) {
            if(startIndex < 0 || startIndex >= Length) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(count < 0 || count > Length - startIndex) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            CharSpan[] sAr = ToCharSpanArray();

            if(sAr.Length == 0) {
                return -1;
            }

            for(int i = 0;i < count;i++) {
                if(sAr[startIndex + i].Character == value) {
                    return startIndex + i;
                }
            }
            return -1;
        }

        public int IndexOf(string value) {
            return IndexOf(value,0);
        }

        public int IndexOf(string value,int startIndex) {
            return IndexOf(value,startIndex,Length - startIndex);
        }

        public int IndexOf(string value,int startIndex,int count) {
            if(startIndex < 0 || startIndex >= Length) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(count < 0 || count > Length - startIndex) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }
            if(value.Length == 0) {
                return -1;
            }

            char[] vAr = value.ToCharArray();
            CharSpan[] sAr = ToCharSpanArray();

            if(vAr.Length == 1) {
                return IndexOf(vAr[0],startIndex,count);
            }

            if(vAr.Length > sAr.Length) {
                return -1;
            }

            // vAr.Length >= 1 && vAr.Length <= sAr.Length
            for(int i = startIndex;i < startIndex + count;i++) {
                if(sAr[i].Character != vAr[0]) {
                    continue;
                }
                for(int j = 1;j < vAr.Length && i + j < sAr.Length;j++) {
                    if(sAr[i + j].Character != vAr[j]) {
                        break;
                    }
                    if(j == vAr.Length - 1) {
                        return i;
                    }
                }
            }
            return -1;
        }

        public int IndexOf(Span value) {
            return IndexOf(value,0);
        }

        public int IndexOf(Span value,int startIndex) {
            return IndexOf(value,startIndex,spans.Length - startIndex);
        }

        public int IndexOf(Span value,int startIndex,int count) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            if(startIndex < 0 || startIndex >= spans.Length) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(count < 0 || count > spans.Length - startIndex) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            for(int i = startIndex;i < count;i++) {
                if(spans[i].Equals(value)) {
                    return i;
                }
            }
            return -1;
        }

        public string ToPlainText() {
            var sb = new StringBuilder();
            foreach(var span in spans) {
                sb.Append(span.Text);
            }
            return sb.ToString();
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
