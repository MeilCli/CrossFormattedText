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

        /// <summary>
        /// call Append(string,Span) method using value.Text value
        /// </summary>
        /// <param name="value">append text and span value</param>
        /// <returns>this instance</returns>
        public FormattedStringBuilder Append(Span value) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }
            return Append(value.Text,value);
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

        public FormattedStringBuilder Clear() {
            Length = 0;
            return this;
        }

        public FormattedStringBuilder Insert(int index,bool value) {
            return Insert(index,value,null);
        }

        public FormattedStringBuilder Insert(int index,bool value,Span span) {
            return Insert(index,value.ToString(),span);
        }

        public FormattedStringBuilder Insert(int index,byte value) {
            return Insert(index,value,null);
        }

        public FormattedStringBuilder Insert(int index,byte value,Span span) {
            return Insert(index,value.ToString(),span);
        }

        public FormattedStringBuilder Insert(int index,char value) {
            return Insert(index,value,null);
        }

        public FormattedStringBuilder Insert(int index,char value,Span span) {
            if(index < 0 || Length < index) {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            EnsureCapacity(Length + 1);

            Array.Copy(values,index,values,index + 1,Length - index);

            span = span ?? new Span();
            values[index] = new CharSpan(value,span);
            Length += 1;

            return this;
        }

        public FormattedStringBuilder Insert(int index,decimal value) {
            return Insert(index,value,null);
        }

        public FormattedStringBuilder Insert(int index,decimal value,Span span) {
            return Insert(index,value.ToString(),span);
        }

        public FormattedStringBuilder Insert(int index,double value) {
            return Insert(index,value,null);
        }

        public FormattedStringBuilder Insert(int index,double value,Span span) {
            return Insert(index,value.ToString(),span);
        }

        public FormattedStringBuilder Insert(int index,float value) {
            return Insert(index,value,null);
        }

        public FormattedStringBuilder Insert(int index,float value,Span span) {
            return Insert(index,value.ToString(),span);
        }

        public FormattedStringBuilder Insert(int index,short value) {
            return Insert(index,value,null);
        }

        public FormattedStringBuilder Insert(int index,short value,Span span) {
            return Insert(index,value.ToString(),span);
        }

        public FormattedStringBuilder Insert(int index,int value) {
            return Insert(index,value,null);
        }

        public FormattedStringBuilder Insert(int index,int value,Span span) {
            return Insert(index,value.ToString(),span);
        }

        public FormattedStringBuilder Insert(int index,long value) {
            return Insert(index,value,null);
        }

        public FormattedStringBuilder Insert(int index,long value,Span span) {
            return Insert(index,value.ToString(),span);
        }

        public FormattedStringBuilder Insert(int index,object value) {
            return Insert(index,value,null);
        }

        public FormattedStringBuilder Insert(int index,object value,Span span) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }
            return Insert(index,value.ToString(),span);
        }

        public FormattedStringBuilder Insert(int index,string value) {
            return Insert(index,value,null);
        }    

        public FormattedStringBuilder Insert(int index,string value,Span span) {
            if(index < 0 || Length < index) {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            EnsureCapacity(Length + value.Length);

            Array.Copy(values,index,values,index + value.Length,Length - index);

            span = span ?? new Span();
            CharSpan[] ar = value.ToCharArray().Select(x => new CharSpan(x,span)).ToArray();

            Array.Copy(ar,0,values,index,ar.Length);
            Length += ar.Length;

            return this;
        }

        public FormattedStringBuilder Remove(int startIndex,int length) {
            if(startIndex < 0) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(length < 0) {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            if(startIndex + length > Length) {
                throw new ArgumentOutOfRangeException($"{nameof(startIndex)} + {nameof(length)}");
            }

            Array.Copy(values,startIndex + length,values,startIndex,Length - (startIndex + length));
            Length -= length;

            return this;
        }

        public FormattedStringBuilder Replace(char oldChar,char newChar) {
            return Replace(oldChar,newChar,null);
        }

        public FormattedStringBuilder Replace(char oldChar,char newChar,Span newSpan) {
            return Replace(oldChar,newChar,newSpan,0,Length);
        }

        public FormattedStringBuilder Replace(char oldChar,char newChar,int startIndex,int count) {
            return Replace(oldChar,newChar,null,startIndex,count);
        }

        public FormattedStringBuilder Replace(char oldChar,char newChar,Span newSpan,int startIndex,int count) {
            if(startIndex < 0) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(count < 0) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if(startIndex + count > Length) {
                throw new ArgumentOutOfRangeException($"{nameof(startIndex)} + {nameof(count)}");
            }

            for(int i = startIndex;i < startIndex + count;i++) {
                if(values[i].Character == oldChar) {
                    values[i] = new CharSpan(newChar,newSpan ?? values[i].Span);
                }
            }

            return this;
        }

        public FormattedStringBuilder Replace(string oldValue,string newValue) {
            return Replace(oldValue,newValue,0,Length);
        }

        public FormattedStringBuilder Replace(string oldValue,string newValue,Span newSpan) {
            return Replace(oldValue,newValue,newSpan,0,Length);
        }

        public FormattedStringBuilder Replace(string oldValue,string newValue,SpanOperand operand) {
            return Replace(oldValue,newValue,operand,0,Length);
        }

        public FormattedStringBuilder Replace(string oldValue,string newValue,int startIndex,int count) {
            return Replace(oldValue,newValue,null,SpanOperand.Left,startIndex,count);
        }

        public FormattedStringBuilder Replace(string oldValue,string newValue,Span newSpan,int startIndex,int count) {
            return Replace(oldValue,newValue,newSpan,SpanOperand.Left,startIndex,count);
        }

        public FormattedStringBuilder Replace(string oldValue,string newValue,SpanOperand operand,int startIndex,int count) {
            return Replace(oldValue,newValue,null,operand,startIndex,count);
        }

        public FormattedStringBuilder Replace(string oldValue,string newValue,Span newSpan,SpanOperand operand,int startIndex,int count) {
            if(startIndex < 0) {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if(count < 0) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if(startIndex + count > Length) {
                throw new ArgumentOutOfRangeException($"{nameof(startIndex)} + {nameof(count)}");
            }
            if(oldValue == null) {
                throw new ArgumentNullException(nameof(oldValue));
            }
            if(newValue == null) {
                newValue = string.Empty;
            }

            int[] replacements = new int[5];
            int replacementsCount = 0;
            
            for(int i = startIndex;i < startIndex + count - (oldValue.Length - 1);) {
                if(startWith(oldValue,i) == false) {
                    i++;
                    continue;
                }

                if(replacementsCount < replacements.Length) {
                    replacements[replacementsCount] = i;
                }else {
                    int[] newArray = new int[replacements.Length * 3 / 2 + 4];
                    Array.Copy(replacements,newArray,replacements.Length);
                    replacements = newArray;
                    replacements[replacementsCount] = i;
                }
                replacementsCount++;

                i += oldValue.Length;
            }

            EnsureCapacity(Length + (newValue.Length - oldValue.Length) * replacementsCount);

            for(int i = replacementsCount - 1;i >= 0;i--) {
                int replacementsStartIndex = replacements[i];

                Array.Copy(
                    values,replacementsStartIndex + oldValue.Length,
                    values,replacementsStartIndex + newValue.Length,
                    Length - (replacementsStartIndex + oldValue.Length));

                if(newSpan == null && operand == SpanOperand.Left) {
                    newSpan = values[i].Span;
                } else if(newSpan == null) {
                    newSpan = values[i + oldValue.Length - 1].Span;
                }

                newSpan.Text = newValue;
                CharSpan[] rAr = newSpan.ToArray();

                Array.Copy(rAr,0,values,replacementsStartIndex,rAr.Length);

                Length += newValue.Length - oldValue.Length;
            }

            return this;
        }

        private bool startWith(string value,int startIndex) {
            if(startIndex + value.Length > Length) {
                return false;
            }

            int index = startIndex;
            foreach(char c in value) {
                if(values[index].Character != c) {
                    return false;
                }
                index++;
            }
            return true;
        }

        public FormattedString ToFormattedString() {
            return MergeCharSpan();
        }
    }
}
