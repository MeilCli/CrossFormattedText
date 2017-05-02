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

        public int EnsureCapacity(int minimumCapacity) {
            if(minimumCapacity > Capacity) {
                expandCapacity(minimumCapacity);
            }
            return Capacity;
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

        public FormattedStringBuilder Append(bool value) {
            return Append(value,null);
        }

        public FormattedStringBuilder Append(bool value,Span span) {
            return Append(value.ToString(),span);
        }

        public FormattedStringBuilder Append(byte value) {
            return Append(value,null);
        }

        public FormattedStringBuilder Append(byte value,Span span) {
            return Append(value.ToString(),span);
        }

        public FormattedStringBuilder Append(char value) {
            return Append(value,null);
        }

        public FormattedStringBuilder Append(char value,Span span) {
            EnsureCapacity(Length + 1);

            span = span ?? new Span();
            values[Length] = new CharSpan(value,span);
            Length += 1;

            return this;
        }

        public FormattedStringBuilder Append(decimal value) {
            return Append(value,null);
        }

        public FormattedStringBuilder Append(decimal value,Span span) {
            return Append(value.ToString(),span);
        }

        public FormattedStringBuilder Append(double value) {
            return Append(value,null);
        }

        public FormattedStringBuilder Append(double value,Span span) {
            return Append(value.ToString(),span);
        }

        public FormattedStringBuilder Append(float value) {
            return Append(value,null);
        }

        public FormattedStringBuilder Append(float value,Span span) {
            return Append(value.ToString(),span);
        }

        public FormattedStringBuilder Append(short value) {
            return Append(value,null);
        }

        public FormattedStringBuilder Append(short value,Span span) {
            return Append(value.ToString(),span);
        }

        public FormattedStringBuilder Append(int value) {
            return Append(value,null);
        }

        public FormattedStringBuilder Append(int value,Span span) {
            return Append(value.ToString(),span);
        }

        public FormattedStringBuilder Append(long value) {
            return Append(value,null);
        }

        public FormattedStringBuilder Append(long value,Span span) {
            return Append(value.ToString(),span);
        }

        public FormattedStringBuilder Append(object value) {
            return Append(value,null);
        }

        public FormattedStringBuilder Append(object value,Span span) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }
            return Append(value.ToString(),span);
        }

        public FormattedStringBuilder Append(string value) {
            return Append(value,null);
        }

        public FormattedStringBuilder Append(string value,Span span) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            EnsureCapacity(Length + value.Length);

            span = span ?? new Span();
            CharSpan[] ar = value.ToCharArray().Select(x => new CharSpan(x,span)).ToArray();

            Array.Copy(ar,0,values,Length,ar.Length);
            Length += ar.Length;

            return this;
        }

        public FormattedStringBuilder AppendLine() {
            return Append(Environment.NewLine);
        }

        public FormattedStringBuilder AppendLine(Span span) {
            return Append(Environment.NewLine,span);
        }

        public FormattedStringBuilder AppendLine(string value) {
            return AppendLine(value,null);
        }

        public FormattedStringBuilder AppendLine(string value,Span span) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            EnsureCapacity(Length + value.Length + Environment.NewLine.Length);

            span = span ?? new Span();
            CharSpan[] ar = (value + Environment.NewLine).ToCharArray().Select(x => new CharSpan(x,span)).ToArray();

            Array.Copy(ar,0,values,Length,ar.Length);
            Length += ar.Length;

            return this;
        }

        public FormattedString ToFormattedString() {
            return MergeCharSpan();
        }
    }
}
