using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plugin.CrossFormattedText.Abstractions;

namespace Test.CrossFormattedText.Unit {

    [TestClass]
    public class SpanColorTest {

        [TestMethod]
        public void EqualsTest() {
            SpanColor a1 = new SpanColor(123,123,123,123);
            SpanColor a2 = new SpanColor(123,123,123,123);
            SpanColor b1 = new SpanColor(100,123,123,123);
            SpanColor b2 = new SpanColor(123,100,123,123);
            SpanColor b3 = new SpanColor(123,123,100,123);
            SpanColor b4 = new SpanColor(123,123,123,100);

            Assert.AreEqual(a1,a2);
            Assert.AreNotEqual(a1,b1);
            Assert.AreNotEqual(a1,b2);
            Assert.AreNotEqual(a1,b3);
            Assert.AreNotEqual(a1,b4);
        }

        [TestMethod]
        public void OperateTest() {
            SpanColor a1 = new SpanColor(123,123,123,123);
            SpanColor a2 = new SpanColor(123,123,123,123);
            SpanColor b1 = new SpanColor(100,123,123,123);
            SpanColor b2 = new SpanColor(123,100,123,123);
            SpanColor b3 = new SpanColor(123,123,100,123);
            SpanColor b4 = new SpanColor(123,123,123,100);

            Assert.AreEqual(a1 == a2,true);
            Assert.AreEqual(a1 != b1,true);
            Assert.AreEqual(a1 != b2,true);
            Assert.AreEqual(a1 != b3,true);
            Assert.AreEqual(a1 != b4,true);
        }
    }
}
