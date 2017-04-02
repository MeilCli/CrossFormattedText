using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossFormattedText.Abstractions {

    public class FormattedString : IEnumerable<EditableSpan> {

        public static readonly FormattedString Empty = new FormattedString();

        private Span[] spans;

        public EditableSpan this[int index] {
            get {
                if(index < 0 || index >= spans.Length) {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return new EditableSpan(spans[index]);
            }
        }

        public int Length => spans.Length;

        private string _plainText;
        public string PlainText {
            get {
                if(_plainText == null) {
                    var sb = new StringBuilder();
                    foreach(var span in spans) {
                        sb.Append(span.Text);
                    }
                    _plainText = sb.ToString();
                }
                return _plainText;
            }
        }

        public FormattedString() {
            spans = new Span[0];
        }

        public FormattedString(IEnumerable<Span> spans) {
            this.spans = spans.Select(x => x.Clone()).ToArray();
        }

        internal FormattedString(Span[] spans) {
            this.spans = spans;
        }

        internal CharSpan[] ToCharSpanArray() {
            var ar = new CharSpan[PlainText.Length];
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
            return PlainText.Contains(value);
        }

        public bool Contains(Span value) {
            return IndexOf(value) != -1;
        }

        public bool EndsWith(string value) {
            return PlainText.EndsWith(value);
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
            return PlainText.IndexOf(value);
        }

        public int IndexOf(char value,int startIndex) {
            return PlainText.IndexOf(value,startIndex);
        }

        public int IndexOf(char value,int startIndex,int count) {
            return PlainText.IndexOf(value,startIndex,count);
        }

        public int IndexOf(string value) {
            return PlainText.IndexOf(value);
        }

        public int IndexOf(string value,int startIndex) {
            return PlainText.IndexOf(value,startIndex);
        }

        public int IndexOf(string value,int startIndex,int count) {
            return PlainText.IndexOf(value,startIndex,count);
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
            if(startIndex < 0 || startIndex >= PlainText.Length) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }
            if(value.Length == 0) {
                return this;
            }

            CharSpan[] sAr = ToCharSpanArray();
            CharSpan[] nAr = new CharSpan[PlainText.Length + value.Length];

            Span newSpan;
            if(operand == SpanOperand.Left && startIndex - 1 >= 0) {
                newSpan = sAr[startIndex - 1].Span;
            } else {
                newSpan = sAr[startIndex].Span;
            }

            CharSpan[] iAr = new CharSpan[value.Length];
            for(int i = 0;i < value.Length;i++) {
                iAr[i] = new CharSpan(value[i],newSpan);
            }

            Array.Copy(sAr,nAr,startIndex);
            Array.Copy(sAr,startIndex,nAr,startIndex + iAr.Length,sAr.Length - startIndex);
            Array.Copy(iAr,0,nAr,startIndex,iAr.Length);

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
            Span newSpan = value.Clone();

            Array.Copy(spans,nAr,insertIndex);
            Array.Copy(spans,insertIndex,nAr,insertIndex + 1,spans.Length - insertIndex);
            nAr[insertIndex] = newSpan;

            return new FormattedString(nAr);
        }

        public int LastIndexOf(char value) {
            return PlainText.LastIndexOf(value);
        }

        public int LastIndexOf(char value,int startIndex) {
            return PlainText.LastIndexOf(value,startIndex);
        }

        public int LastIndexOf(char value,int startIndex,int count) {
            return PlainText.LastIndexOf(value,startIndex,count);
        }

        public int LastIndexOf(string value) {
            return PlainText.LastIndexOf(value);
        }

        public int LastIndexOf(string value,int startIndex) {
            return PlainText.LastIndexOf(value,startIndex);
        }

        public int LastIndexOf(string value,int startIndex,int count) {
            return PlainText.LastIndexOf(value,startIndex,count);
        }

        public int LastIndexOf(Span value) {
            return LastIndexOf(value,Length - 1);
        }

        public int LastIndexOf(Span value,int startIndex) {
            return LastIndexOf(value,startIndex,startIndex + 1);
        }

        public int LastIndexOf(Span value,int startIndex,int count) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            if(startIndex < 0 || startIndex >= spans.Length) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(count < 0 || startIndex - count + 1 < 0) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            int index = startIndex;
            for(int i = 0;i < count;i++) {
                if(spans[index].Equals(value)) {
                    return index;
                }
                index--;
            }
            return -1;
        }

        public FormattedString Remove(int startIndex) {
            return Remove(startIndex,PlainText.Length - startIndex);
        }

        public FormattedString Remove(int startIndex,int count) {
            if(startIndex < 0) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(count < 0) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if(startIndex + count > PlainText.Length) {
                throw new ArgumentOutOfRangeException($"{nameof(startIndex)} + {nameof(count)}");
            }

            CharSpan[] sAr = ToCharSpanArray();
            CharSpan[] nAr = new CharSpan[PlainText.Length - count];

            Array.Copy(sAr,nAr,startIndex);
            Array.Copy(sAr,startIndex + count,nAr,startIndex,PlainText.Length - startIndex - count);

            return MergeCharSpan(nAr);
        }

        public bool AnySpanReferenceEquals(FormattedString formattedString) {
            foreach(var span1 in spans) {
                foreach(var span2 in formattedString.spans) {
                    if(ReferenceEquals(span1,span2)) {
                        return true;
                    }
                }
            }
            return false;
        }

        public IEnumerator<EditableSpan> GetEnumerator() {
            foreach(var span in spans) {
                yield return new EditableSpan(span);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
