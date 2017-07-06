using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossFormattedText.Abstractions {

    public class FormattedStringConverter {

        private static readonly List<IAttributeConverter> atrributeConverters = new List<IAttributeConverter>() {
            new FontSizeAttributeConverter(),
            new FontAttributeConverter(),
            ColorAttributeConverter.ForegroundConverter,
            ColorAttributeConverter.BackgroundConverter
        };

        public FormattedString DeserializeObject(string value) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }
            if(value.Contains("<span") == false) {
                return value; // implicit cast
            }

            var fsb = new FormattedStringBuilder();

            int i = 0;
            while(i < value.Length) {
                if(value.Substring(i).StartsWith("<span") == false) {
                    int startTagIndex = value.IndexOf("<span",i);
                    if(startTagIndex == -1) {
                        fsb.Append(decode(value.Substring(i,value.Length - i)));
                        break;
                    }
                    fsb.Append(decode(value.Substring(i,startTagIndex - i)));
                    i = startTagIndex;
                    continue;
                }

                // start tag
                int startTagEndIndex = value.IndexOf('>',i);
                if(startTagEndIndex == -1) {
                    break; // broken value
                }
                var span = new Span();
                string attributes = value.Substring(i + 5,startTagEndIndex - (i + 5));
                foreach(var attribute in attributes.Split(new[] { ' ' },StringSplitOptions.RemoveEmptyEntries)) {
                    int equalIndex = attribute.IndexOf('=');
                    if(equalIndex == -1) {
                        continue;
                    }
                    string attributeName = attribute.Substring(0,equalIndex);
                    string attributeValue = attribute.Substring(equalIndex).Trim('=','"');
                    var converter = atrributeConverters.Where(x => x.Name == attributeName).SingleOrDefault();
                    converter?.DeserializeValue(span,attributeValue);
                }

                // text
                int endTagIndex = value.IndexOf("</span>");
                if(endTagIndex == -1) {
                    break;
                }
                string text = value.Substring(startTagEndIndex + 1,endTagIndex - (startTagEndIndex + 1));
                span.Text = decode(text);
                fsb.Append(span);
                i = endTagIndex + 7;
            }

            return fsb.ToFormattedString();
        }

        public string SerializeObject(FormattedString value) {
            if(value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            var sb = new StringBuilder();
            foreach(var span in value) {
                // start tag
                sb.Append("<span");
                var attrs = atrributeConverters
                    .Select(x => new { name = x.Name,value = x.SerializeValue(span) })
                    .Where(x => x.value != null);
                foreach(var attr in attrs) {
                    sb.Append($" {attr.name}=\"{attr.value}\"");
                }
                sb.Append(">");

                // text
                sb.Append(encode(span.Text));

                //end tag
                sb.Append("</span>");
            }
            return sb.ToString();
        }

        private string encode(string value) {
            return value.Replace("\r\n","<br>").Replace("\n","<br>").Replace("\r","<br>");
        }

        private string decode(string value) {
            return value.Replace("<br>","\n");
        }
    }

    interface IAttributeConverter {

        string Name { get; }

        void DeserializeValue(Span span,string value);

        string SerializeValue(EditableSpan span);
    }

    class FontSizeAttributeConverter : IAttributeConverter {

        private const string ultraBig = "ultra-big";
        private const string big = "big";
        private const string normal = "normal";
        private const string small = "small";
        private const string ultraSmall = "ultra-small";

        public string Name { get; } = "font-size";

        public void DeserializeValue(Span span,string value) {
            var fontSize = FontSize.Normal;
            switch(value) {
                case ultraBig:
                    fontSize = FontSize.UltraBig;
                    break;
                case big:
                    fontSize = FontSize.Big;
                    break;
                case normal:
                    fontSize = FontSize.Normal;
                    break;
                case small:
                    fontSize = FontSize.Small;
                    break;
                case ultraSmall:
                    fontSize = FontSize.UltraSmall;
                    break;
                default:
                    if(value == null) {
                        break;
                    }
                    float proportion;
                    if(float.TryParse(value,out proportion)) {
                        fontSize = new FontSize(proportion);
                    }
                    break;
            }
            span.FontSize = fontSize;
        }

        public string SerializeValue(EditableSpan span) {
            var fontSize = span.FontSize;
            if(fontSize == FontSize.UltraBig) {
                return ultraBig;
            }
            if(fontSize == FontSize.Big) {
                return big;
            }
            if(fontSize == FontSize.Normal) {
                return null; // initial value
            }
            if(fontSize == FontSize.Small) {
                return small;
            }
            if(fontSize == FontSize.UltraSmall) {
                return ultraSmall;
            }
            return span.FontSize.Proportion.ToString();
        }
    }

    class FontAttributeConverter : IAttributeConverter {

        private const string none = "none";
        private const string bold = "bold";
        private const string italic = "italic";
        private const string italicBold = "italic-bold";

        public string Name { get; } = "font";

        public void DeserializeValue(Span span,string value) {
            var font = FontAttributes.None;
            switch(value) {
                case bold:
                    font = FontAttributes.Bold;
                    break;
                case italic:
                    font = FontAttributes.Italic;
                    break;
                case italicBold:
                    font = FontAttributes.Italic | FontAttributes.Bold;
                    break;
                case none:
                default:
                    break;
            }
            span.FontAttributes = font;
        }

        public string SerializeValue(EditableSpan span) {
            var font = span.FontAttributes;
            if(font == FontAttributes.Bold) {
                return bold;
            }
            if(font == FontAttributes.Italic) {
                return italic;
            }
            if(font == (FontAttributes.Italic | FontAttributes.Bold)) {
                return italicBold;
            }
            return null;
        }
    }

    class ColorAttributeConverter : IAttributeConverter {

        private const string foregroundColorName = "f-color";
        private const string backgrounfColorName = "b-color";

        private static ColorAttributeConverter _foregroundConveter;
        public static ColorAttributeConverter ForegroundConverter {
            get {
                if(_foregroundConveter == null) {
                    _foregroundConveter = new ColorAttributeConverter(foregroundColorName);
                }
                return _foregroundConveter;
            }
        }

        private static ColorAttributeConverter _backgroundConveter;
        public static ColorAttributeConverter BackgroundConverter {
            get {
                if(_backgroundConveter == null) {
                    _backgroundConveter = new ColorAttributeConverter(backgrounfColorName);
                }
                return _backgroundConveter;
            }
        }

        public string Name { get; }

        private ColorAttributeConverter(string name) {
            Name = name;
        }

        public void DeserializeValue(Span span,string value) {
            string[] rawValues = value.Split(',');
            if(rawValues.Length != 4) {
                return;
            }

            int a;
            int r;
            int g;
            int b;
            if(int.TryParse(rawValues[0],out a) && int.TryParse(rawValues[1],out r)
                && int.TryParse(rawValues[2],out g) && int.TryParse(rawValues[3],out b)) {
                var color = new SpanColor(a,r,g,b);
                switch(Name) {
                    case foregroundColorName:
                        span.ForegroundColor = color;
                        break;
                    case backgrounfColorName:
                        span.BackgroundColor = color;
                        break;
                }
            }
        }

        public string SerializeValue(EditableSpan span) {
            SpanColor color;
            if(Name == foregroundColorName) {
                color = span.ForegroundColor;
            } else {
                color = span.BackgroundColor;
            }
            if(color == SpanColor.DefaultValue) {
                return null;
            }
            return $"{color.Alpha},{color.Red},{color.Green},{color.Blue}";
        }
    }
}
