using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plugin.CrossFormattedText.Abstractions;

namespace Test.CrossFormattedText.Unit {

    [TestClass]
    public class FormattedStringConverterTest {

        private FormattedStringConverter converter = new FormattedStringConverter();

        private bool spanEquals(FormattedString a,FormattedString b) {
            if(a == null || b == null) {
                return false;
            }
            if(a.Length != b.Length) {
                return false;
            }
            if(a == b) {
                return true;
            }
            foreach(var s in a.Zip(b,(x,y) => new { x = x,y = y })) {
                if(s.x.Equals(s.y) == false) {
                    return false;
                }
            }
            return true;
        }

        [TestMethod]
        public void FontSizeTest() {
            var span = new Span() { Text = "test\ntest",FontSize = FontSize.UltraBig };

            FormattedString a = new FormattedStringBuilder().Append(span).ToFormattedString();
            var aText = "<span font-size=\"ultra-big\">test<br>test</span>";

            span.FontSize = FontSize.Normal;
            FormattedString b = new FormattedStringBuilder().Append(span).ToFormattedString();
            var bText = "<span>test<br>test</span>";

            Assert.AreEqual(converter.SerializeObject(a),aText);
            Assert.AreEqual(converter.SerializeObject(b),bText);

            Assert.AreEqual(spanEquals(a,converter.DeserializeObject(converter.SerializeObject(a))),true);
            Assert.AreEqual(spanEquals(b,converter.DeserializeObject(converter.SerializeObject(b))),true);
        }

        [TestMethod]
        public void FontTest() {
            var span = new Span() { Text = "text",FontAttributes = FontAttributes.Bold };

            FormattedString a = new FormattedStringBuilder().Append(span).ToFormattedString();
            var aText = "<span font=\"bold\">text</span>";

            span.FontAttributes = FontAttributes.Bold | FontAttributes.Italic;
            FormattedString b = new FormattedStringBuilder().Append(span).ToFormattedString();
            var bText = "<span font=\"italic-bold\">text</span>";

            Assert.AreEqual(converter.SerializeObject(a),aText);
            Assert.AreEqual(converter.SerializeObject(b),bText);

            Assert.AreEqual(spanEquals(a,converter.DeserializeObject(converter.SerializeObject(a))),true);
            Assert.AreEqual(spanEquals(b,converter.DeserializeObject(converter.SerializeObject(b))),true);
        }

        [TestMethod]
        public void ColorTest() {
            var span = new Span() { Text = "text",ForegroundColor = new SpanColor(1,2,3,4) };

            FormattedString a = new FormattedStringBuilder().Append(span).ToFormattedString();
            var aText = "<span f-color=\"1,2,3,4\">text</span>";

            span.ForegroundColor = SpanColor.DefaultValue;
            span.BackgroundColor = new SpanColor(4,3,2,1);
            FormattedString b = new FormattedStringBuilder().Append(span).ToFormattedString();
            var bText = "<span b-color=\"4,3,2,1\">text</span>";

            Assert.AreEqual(converter.SerializeObject(a),aText);
            Assert.AreEqual(converter.SerializeObject(b),bText);

            Assert.AreEqual(spanEquals(a,converter.DeserializeObject(converter.SerializeObject(a))),true);
            Assert.AreEqual(spanEquals(b,converter.DeserializeObject(converter.SerializeObject(b))),true);
        }
    }
}
