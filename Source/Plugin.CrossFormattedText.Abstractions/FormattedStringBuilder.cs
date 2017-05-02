using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossFormattedText.Abstractions {

    public class FormattedStringBuilder {

        private CharSpan[] values;

        public int Capacity => values.Length;
        public int Length { get; private set; }

        public FormattedStringBuilder() : this(0) { }

        public FormattedStringBuilder(int capacity) {
            values = new CharSpan[capacity];
        }

        private void expandCapacity(int minimumCapacity) {
            int newCapacity = (Capacity + 1) * 2;
            if(minimumCapacity > newCapacity) {
                newCapacity = minimumCapacity;
            }

            var newArray = new CharSpan[newCapacity];
            Array.Copy(values,newArray,Length);
            values = newArray;
        }

        public void EnsureCapacity(int minimumCapacity) {
            if(minimumCapacity > Capacity) {
                expandCapacity(minimumCapacity);
            }
        }

        internal FormattedString MergeCharSpan() {
            var list = new List<Span>();
            Span currentSpan = null;
            var sb = new StringBuilder();
            for(int i = 0;i < Length;i++) {
                CharSpan c = values[i];
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

        public void Append(bool value) {
            Append(value,null);
        }

        public void Append(bool value,Span span) {
            Append(value.ToString(),span);
        }

        public void Append(byte value) {
            Append(value,null);
        }

        public void Append(byte value,Span span) {
            Append(value.ToString(),span);
        }

        public void Append(char value) {
            Append(value,null);
        }

        public void Append(char value,Span span) {
            EnsureCapacity(Length + 1);

            span = span ?? new Span();
            values[Length] = new CharSpan(value,span);
            Length += 1;
        }

        public void Append(decimal value) {
            Append(value,null);
        }

        public void Append(decimal value,Span span) {
            Append(value.ToString(),span);
        }

        public void Append(double value) {
            Append(value,null);
        }

        public void Append(double value,Span span) {
            Append(value.ToString(),span);
        }

        public void Append(float value) {
            Append(value,null);
        }

        public void Append(float value,Span span) {
            Append(value.ToString(),span);
        }

        public void Append(short value) {
            Append(value,null);
        }

        public void Append(short value,Span span) {
            Append(value.ToString(),span);
        }

        public void Append(int value) {
            Append(value,null);
        }

        public void Append(int value,Span span) {
            Append(value.ToString(),span);
        }

        public void Append(long value) {
            Append(value,null);
        }

        public void Append(long value,Span span) {
            Append(value.ToString(),span);
        }

        public void Append(object value) {
            Append(value,null);
        }

        public void Append(object value,Span span) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }
            Append(value.ToString(),span);
        }

        public void Append(string value) {
            Append(value,null);
        }

        public void Append(string value,Span span) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            EnsureCapacity(Length + value.Length);

            span = span ?? new Span();
            CharSpan[] ar = value.ToCharArray().Select(x => new CharSpan(x,span)).ToArray();

            Array.Copy(ar,0,values,Length,ar.Length);
            Length += ar.Length;
        }

        public void AppendLine() {
            Append(Environment.NewLine);
        }

        public void AppendLine(Span span) {
            Append(Environment.NewLine,span);
        }

        public void AppendLine(string value) {
            AppendLine(value,null);
        }

        public void AppendLine(string value,Span span) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            EnsureCapacity(Length + value.Length + Environment.NewLine.Length);

            span = span ?? new Span();
            CharSpan[] ar = (value + Environment.NewLine).ToCharArray().Select(x => new CharSpan(x,span)).ToArray();

            Array.Copy(ar,0,values,Length,ar.Length);
            Length += ar.Length;
        }

        public FormattedString ToFormattedString() {
            return MergeCharSpan();
        }
    }
}
