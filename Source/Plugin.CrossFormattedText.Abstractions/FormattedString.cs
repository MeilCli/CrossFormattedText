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

        public FormattedString(IEnumerable<Span> spans) {
            this.spans = spans.Select(x => x.Clone()).ToArray();
        }

        internal FormattedString(Span[] spans) {
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

        public int Length => spans.Length;

        private int? _textLength;
        public int TextLength {
            get {
                if(_textLength != null) {
                    return _textLength.Value;
                }

                int size = 0;
                foreach(var span in spans) {
                    size += span.Text.Length;
                }
                _textLength = size;
                return _textLength.Value;
            }
        }

        internal CharSpan[] ToCharSpanArray() {
            var ar = new CharSpan[TextLength];
            int index = 0;
            for(int i = 0;i < spans.Length;i++) {
                Span span = spans[i];
                foreach(var c in span.Text) {
                    ar[index] = new CharSpan(c,span);
                    index++;
                }
            }
            return ar;
        }

        internal FormattedString MergeCharSpan(CharSpan[] newSpans) {
            var list = new List<Span>();
            Span currentSpan = null;
            var sb = new StringBuilder();
            for(int i = 0;i < newSpans.Length;i++) {
                CharSpan c = newSpans[i];
                if(currentSpan != null && ReferenceEquals(currentSpan,c.Span) == false) {
                    Span span = currentSpan.Clone();
                    span.Text = sb.ToString();
                    sb.Clear();
                    currentSpan = null;
                    list.Add(span);
                }
                if(currentSpan == null) {
                    currentSpan = c.Span;
                }
                sb.Append(c.Character);
            }
            if(currentSpan != null) {
                Span span = currentSpan.Clone();
                span.Text = sb.ToString();
                list.Add(span);
            }
            return new FormattedString(list.ToArray());
        }

        public bool Contains(string value) {
            if(value?.Length == 0) {
                return true;
            }
            return IndexOf(value) != -1;
        }

        public bool Contains(Span value) {
            return IndexOf(value) != -1;
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
            return IndexOf(value,startIndex,TextLength - startIndex);
        }

        public int IndexOf(char value,int startIndex,int count) {
            if(startIndex < 0 || startIndex >= TextLength) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(count < 0 || count > TextLength - startIndex) {
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
            return IndexOf(value,startIndex,TextLength - startIndex);
        }

        public int IndexOf(string value,int startIndex,int count) {
            if(startIndex < 0 || startIndex >= TextLength) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(count < 0 || count > TextLength - startIndex) {
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

        public FormattedString Insert(int startIndex,string value) {
            return Insert(startIndex,value,SpanOperand.Right);
        }

        public FormattedString Insert(int startIndex,string value,SpanOperand operand) {
            if(startIndex < 0 || startIndex >= TextLength) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }
            if(value.Length == 0) {
                return this;
            }

            CharSpan[] sAr = ToCharSpanArray();
            CharSpan[] nAr = new CharSpan[TextLength + value.Length];

            Span newSpan;
            if(operand == SpanOperand.Left && startIndex - 1 >= 0) {
                newSpan = sAr[startIndex - 1].Span;
            } else {
                newSpan = sAr[startIndex].Span;
            }
            int index = 0;
            for(int i = 0;i < sAr.Length;i++) {
                if(i == startIndex) {
                    foreach(var c in value) {
                        nAr[index] = new CharSpan(c,newSpan);
                        index++;
                    }
                }
                nAr[index] = sAr[i];
                index++;
            }

            return MergeCharSpan(nAr);
        }

        public FormattedString Insert(int insertIndex,Span value) {
            if(insertIndex < 0 || insertIndex >= Length) {
                throw new ArgumentOutOfRangeException(nameof(insertIndex));
            }
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            Span[] nAr = new Span[spans.Length + 1];
            int index = 0;
            for(int i = 0;i < spans.Length;i++) {
                if(i == insertIndex) {
                    nAr[index] = value.Clone();
                    index++;
                }
                nAr[index] = spans[i].Clone();
                index++;
            }

            return new FormattedString(nAr);
        }

        public string ToPlainText() {
            var sb = new StringBuilder();
            foreach(var span in spans) {
                sb.Append(span.Text);
            }
            return sb.ToString();
        }

        public bool AnySpanReferenceEquals(FormattedString formattedString) {
            if(spans.Length != formattedString.spans.Length) {
                throw new InvalidOperationException();
            }
            for(int i = 0;i < spans.Length;i++) {
                if(ReferenceEquals(spans[i],formattedString.spans[i])) {
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<Span> GetEnumerator() {
            foreach(var span in spans) {
                yield return span.Clone();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
