using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

        private string _text;
        public string Text {
            get {
                if(_text == null) {
                    var sb = new StringBuilder();
                    foreach(var span in spans) {
                        sb.Append(span.Text);
                    }
                    _text = sb.ToString();
                }
                return _text;
            }
        }

        public int TextLength => Text.Length;

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

        internal Span[] ToClonedSpanArray() {
            var ar = new Span[spans.Length];
            for(int i = 0;i < spans.Length;i++) {
                ar[i] = spans[i].Clone();
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
            return Text.Contains(value);
        }

        public bool ContainsSpan(Span value) {
            return IndexOfSpan(value) != -1;
        }

        public bool EndsWith(string value) {
            return Text.EndsWith(value);
        }

        public bool EndsWithSpan(Span value) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }
            if(spans.Length == 0) {
                return false;
            }

            return spans[spans.Length - 1].Equals(value);
        }

        public int IndexOf(char value) {
            return Text.IndexOf(value);
        }

        public int IndexOf(char value,int startIndex) {
            return Text.IndexOf(value,startIndex);
        }

        public int IndexOf(char value,int startIndex,int count) {
            return Text.IndexOf(value,startIndex,count);
        }

        public int IndexOf(string value) {
            return Text.IndexOf(value);
        }

        public int IndexOf(string value,int startIndex) {
            return Text.IndexOf(value,startIndex);
        }

        public int IndexOf(string value,int startIndex,int count) {
            return Text.IndexOf(value,startIndex,count);
        }

        public int IndexOfSpan(Span value) {
            return IndexOfSpan(value,0);
        }

        public int IndexOfSpan(Span value,int startIndex) {
            return IndexOfSpan(value,startIndex,spans.Length - startIndex);
        }

        public int IndexOfSpan(Span value,int startIndex,int count) {
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
            return Insert(startIndex,value,null,operand);
        }

        public FormattedString Insert(int startIndex,Span span) {
            if(span == null) {
                throw new ArgumentNullException(nameof(span));
            }
            return Insert(startIndex,span.Text,span);
        }

        public FormattedString Insert(int startIndex,string value,Span span) {
            return Insert(startIndex,value,span,SpanOperand.Right);
        }

        internal FormattedString Insert(int startIndex,string value,Span span,SpanOperand operand) {
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

            CharSpan[] iAr = new CharSpan[value.Length];
            for(int i = 0;i < value.Length;i++) {
                iAr[i] = new CharSpan(value[i],newSpan);
            }

            Array.Copy(sAr,nAr,startIndex);
            Array.Copy(sAr,startIndex,nAr,startIndex + iAr.Length,sAr.Length - startIndex);
            Array.Copy(iAr,0,nAr,startIndex,iAr.Length);

            return MergeCharSpan(nAr);
        }

        public FormattedString InsertSpan(int insertIndex,Span value) {
            if(insertIndex < 0 || insertIndex >= Length) {
                throw new ArgumentOutOfRangeException(nameof(insertIndex));
            }
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            Span[] sAr = ToClonedSpanArray();
            Span[] nAr = new Span[spans.Length + 1];
            Span newSpan = value.Clone();

            Array.Copy(sAr,nAr,insertIndex);
            Array.Copy(sAr,insertIndex,nAr,insertIndex + 1,sAr.Length - insertIndex);
            nAr[insertIndex] = newSpan;

            return new FormattedString(nAr);
        }

        public int LastIndexOf(char value) {
            return Text.LastIndexOf(value);
        }

        public int LastIndexOf(char value,int startIndex) {
            return Text.LastIndexOf(value,startIndex);
        }

        public int LastIndexOf(char value,int startIndex,int count) {
            return Text.LastIndexOf(value,startIndex,count);
        }

        public int LastIndexOf(string value) {
            return Text.LastIndexOf(value);
        }

        public int LastIndexOf(string value,int startIndex) {
            return Text.LastIndexOf(value,startIndex);
        }

        public int LastIndexOf(string value,int startIndex,int count) {
            return Text.LastIndexOf(value,startIndex,count);
        }

        public int LastIndexOfSpan(Span value) {
            return LastIndexOfSpan(value,Length - 1);
        }

        public int LastIndexOfSpan(Span value,int startIndex) {
            return LastIndexOfSpan(value,startIndex,startIndex + 1);
        }

        public int LastIndexOfSpan(Span value,int startIndex,int count) {
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
            return Remove(startIndex,TextLength - startIndex);
        }

        public FormattedString Remove(int startIndex,int count) {
            if(startIndex < 0) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(count < 0) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if(startIndex + count > TextLength) {
                throw new ArgumentOutOfRangeException($"{nameof(startIndex)} + {nameof(count)}");
            }

            CharSpan[] sAr = ToCharSpanArray();
            CharSpan[] nAr = new CharSpan[TextLength - count];

            Array.Copy(sAr,nAr,startIndex);
            Array.Copy(sAr,startIndex + count,nAr,startIndex,TextLength - startIndex - count);

            return MergeCharSpan(nAr);
        }

        public FormattedString RemoveSpan(int startIndex) {
            return RemoveSpan(startIndex,Length - startIndex);
        }

        public FormattedString RemoveSpan(int startIndex,int count) {
            if(startIndex < 0) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(count < 0) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if(startIndex + count > Length) {
                throw new ArgumentOutOfRangeException($"{nameof(startIndex)} + {nameof(count)}");
            }

            Span[] sAr = ToClonedSpanArray();
            Span[] nAr = new Span[Length - count];

            Array.Copy(sAr,nAr,startIndex);
            Array.Copy(sAr,startIndex + count,nAr,startIndex,Length - startIndex - count);

            return new FormattedString(nAr);
        }

        public FormattedString Replace(char oldChar,char newChar) {
            return Replace(oldChar,newChar,null);
        }

        public FormattedString Replace(char oldChar,char newChar,Span newSpan) {
            CharSpan[] sAr = ToCharSpanArray();

            for(int i = 0;i < sAr.Length;i++) {
                if(sAr[i].Character == oldChar) {
                    sAr[i] = new CharSpan(newChar,newSpan ?? sAr[i].Span);
                }
            }

            return MergeCharSpan(sAr);
        }

        public FormattedString Replace(string oldValue,string newValue) {
            return Replace(oldValue,newValue,SpanOperand.Right);
        }

        public FormattedString Replace(string oldValue,string newValue,SpanOperand operand) {
            return Replace(oldValue,newValue,null,operand);
        }

        public FormattedString Replace(string oldValue,Span newSpan) {
            if(newSpan == null) {
                throw new ArgumentNullException(nameof(newSpan));
            }
            return Replace(oldValue,newSpan.Text,newSpan);
        }

        public FormattedString Replace(string oldValue,string newValue,Span newSpan) {
            return Replace(oldValue,newValue,newSpan,SpanOperand.Right);
        }

        internal FormattedString Replace(string oldValue,string newValue,Span newSpan,SpanOperand operand) {
            if(oldValue == null) {
                throw new ArgumentNullException(nameof(oldValue));
            }
            if(oldValue.Length == 0) {
                throw new ArgumentException(nameof(oldValue));
            }
            if(newValue == null) {
                newValue = string.Empty;
            }

            int replaceCount = 0;
            int offset = 0;
            while((offset = Text.IndexOf(oldValue,offset)) != -1) {
                replaceCount++;
                offset += oldValue.Length;
            }

            CharSpan[] sAr = ToCharSpanArray();
            CharSpan[] nAr = new CharSpan[sAr.Length + (newValue.Length - oldValue.Length) * replaceCount];

            int oldIndex = 0;
            int newIndex = 0;
            for(int i = 0;i < replaceCount;i++) {
                int index = Text.IndexOf(oldValue,oldIndex);
                int copySize = index - oldIndex;

                Array.Copy(sAr,oldIndex,nAr,newIndex,copySize);

                oldIndex += copySize + oldValue.Length;
                newIndex += copySize;

                if(newSpan == null && operand == SpanOperand.Left) {
                    newSpan = sAr[index].Span;
                } else if(newSpan == null) {
                    newSpan = sAr[index + oldValue.Length - 1].Span;
                }

                newSpan = newSpan.Clone();
                newSpan.Text = newValue;
                CharSpan[] rAr = newSpan.ToArray();

                Array.Copy(rAr,0,nAr,newIndex,rAr.Length);
                newIndex += rAr.Length;
            }

            Array.Copy(sAr,oldIndex,nAr,newIndex,TextLength - oldIndex);

            return MergeCharSpan(nAr);
        }

        public FormattedString ReplaceSpan(Span oldValue,Span newValue) {
            if(oldValue == null) {
                throw new ArgumentNullException(nameof(oldValue));
            }
            if(newValue == null) {
                throw new ArgumentNullException(nameof(newValue));
            }

            Span[] sAr = ToClonedSpanArray();
            for(int i = 0;i < sAr.Length;i++) {
                if(sAr[i].Equals(oldValue)) {
                    sAr[i] = newValue.Clone();
                }
            }

            return new FormattedString(sAr);
        }

        public FormattedString[] Split(params char[] separator) {
            return Split(separator,TextLength);
        }

        public FormattedString[] Split(char[] separator,int count) {
            if(count < 0) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            string[] tAr = Text.Split(separator,count,StringSplitOptions.RemoveEmptyEntries);

            return split(tAr);
        }

        public FormattedString[] Split(params string[] separator) {
            return Split(separator,TextLength);
        }

        public FormattedString[] Split(string[] separator,int count) {
            if(count < 0) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            string[] tAr = Text.Split(separator,count,StringSplitOptions.RemoveEmptyEntries);

            return split(tAr);
        }

        private FormattedString[] split(string[] tAr) {
            FormattedString[] result = new FormattedString[tAr.Length];

            int index = 0;
            for(int i = 0;i < tAr.Length;i++) {
                string value = tAr[i];
                index = IndexOf(value,index);
                result[i] = Substring(index,value.Length);
                index += value.Length;
            }

            return result;
        }

        public FormattedString[] SplitSpan(params Span[] separator) {
            return SplitSpan(separator,Length);
        }

        public FormattedString[] SplitSpan(Span[] separator,int count) {
            if(count < 0) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if(separator == null) {
                return new FormattedString[] { this };
            }

            Span[] sAr = ToClonedSpanArray();
            List<Span> list = new List<Span>();
            List<FormattedString> result = new List<FormattedString>();

            for(int i = 0;i < sAr.Length;i++) {
                Span span = sAr[i];
                if(separator.All(x => x.Equals(span) == false)) {
                    list.Add(span);
                } else if(list.Count > 0) {
                    result.Add(new FormattedString(list.ToArray()));
                    list.Clear();
                }
            }
            if(list.Count > 0) {
                result.Add(new FormattedString(list.ToArray()));
            }

            return result.ToArray();
        }

        public bool StartsWith(string value) {
            return Text.StartsWith(value);
        }

        public bool StartsWithSpan(Span value) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }
            if(spans.Length == 0) {
                return false;
            }
            return spans[0].Equals(value);
        }

        public FormattedString Substring(int startIndex) {
            return Substring(startIndex,TextLength - startIndex);
        }

        public FormattedString Substring(int startIndex,int length) {
            if(startIndex < 0) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(startIndex + length > TextLength) {
                throw new ArgumentOutOfRangeException($"{nameof(startIndex)} + {nameof(length)}");
            }

            CharSpan[] sAr = ToCharSpanArray();
            CharSpan[] nAr = new CharSpan[length];

            Array.Copy(sAr,startIndex,nAr,0,length);

            return MergeCharSpan(nAr);
        }

        public FormattedString Subspan(int startIndex) {
            return Subspan(startIndex,Length - startIndex);
        }

        public FormattedString Subspan(int startIndex,int length) {
            if(startIndex < 0) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(startIndex + length > Length) {
                throw new ArgumentOutOfRangeException($"{nameof(startIndex)} + {nameof(length)}");
            }

            Span[] sAr = ToClonedSpanArray();
            Span[] nAr = new Span[length];

            Array.Copy(sAr,startIndex,nAr,0,length);

            return new FormattedString(nAr);
        }

        public FormattedString ToLower() {
            Span[] sAr = ToClonedSpanArray();

            foreach(Span span in sAr) {
                span.Text = span.Text.ToLower();
            }

            return new FormattedString(sAr);
        }

        public FormattedString ToLowerInvariant() {
            Span[] sAr = ToClonedSpanArray();

            foreach(Span span in sAr) {
                span.Text = span.Text.ToLowerInvariant();
            }

            return new FormattedString(sAr);
        }

        public FormattedString ToUpper() {
            Span[] sAr = ToClonedSpanArray();

            foreach(Span span in sAr) {
                span.Text = span.Text.ToUpper();
            }

            return new FormattedString(sAr);
        }

        public FormattedString ToUpperInvariant() {
            Span[] sAr = ToClonedSpanArray();

            foreach(Span span in sAr) {
                span.Text = span.Text.ToUpperInvariant();
            }

            return new FormattedString(sAr);
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
