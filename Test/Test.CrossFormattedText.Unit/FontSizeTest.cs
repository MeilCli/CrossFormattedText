using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plugin.CrossFormattedText.Abstractions;

namespace Test.CrossFormattedText.Unit {

    [TestClass]
    public class FontSizeTest {

        [TestMethod]
        public void EqualsTest() {
            FontSize a1 = new FontSize(1.1f);
            FontSize a2 = new FontSize(1.1f);
            FontSize b = new FontSize(1.11f);
            Assert.AreEqual(a1,a2);
            Assert.AreNotEqual(a1,b);
        }

        [TestMethod]
        public void OperateTest() {
            FontSize a1 = new FontSize(1.1f);
            FontSize a2 = new FontSize(1.1f);
            FontSize b = new FontSize(1.11f);
            Assert.AreEqual(a1 == a2,true);
            Assert.AreEqual(a1 != b,true);
        }
    }
}
