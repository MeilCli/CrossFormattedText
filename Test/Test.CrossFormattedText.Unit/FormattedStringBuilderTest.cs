using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plugin.CrossFormattedText.Abstractions;

namespace Test.CrossFormattedText.Unit {

    [TestClass]
    public class FormattedStringBuilderTest {

        [TestMethod]
        public void AppendTest() {
            var sb = new StringBuilder();
            var fsb = new FormattedStringBuilder();

            sb.Append(true);
            fsb.Append(true);
            sb.Append((byte)10);
            fsb.Append((byte)10);
            sb.Append((decimal)10.0);
            fsb.Append((decimal)10.0);
            sb.Append(3.14);
            fsb.Append(3.14);
            sb.Append(3.14f);
            fsb.Append(3.14f);
            sb.Append((short)10);
            fsb.Append((short)10);
            sb.Append(19);
            fsb.Append(19);
            sb.Append(1L);
            fsb.Append(1L);
            sb.Append((object)"aaa");
            fsb.Append((object)"aaa");
            sb.Append('1');
            fsb.Append('1');
            sb.Append("text text");
            fsb.Append("text text");

            Assert.AreEqual(fsb.ToFormattedString().Text,sb.ToString());
        }

        [TestMethod]
        public void AppendLineTest() {
            var sb = new StringBuilder();
            var fsb = new FormattedStringBuilder();

            sb.AppendLine();
            fsb.AppendLine();
            sb.AppendLine("aaa");
            fsb.AppendLine("aaa");

            Assert.AreEqual(fsb.ToFormattedString().Text,sb.ToString());
        }
    }
}
