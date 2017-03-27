using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plugin.CrossFormattedText.Abstractions;

namespace Test.CrossFormattedText.Unit {
    
    [TestClass]
    public class SpanTest {
       
        [TestMethod]
        public void CloneTest() {
            Span span = new Span() {
                Text = "tt"
            };
            Span clonedSpan = span.Clone();
            Assert.AreEqual(ReferenceEquals(span,clonedSpan),false);
            Assert.AreEqual(ReferenceEquals(span.BackgroundColor,clonedSpan.BackgroundColor),false);
            Assert.AreEqual(ReferenceEquals(span.ForegroundColor,clonedSpan.ForegroundColor),false);
            Assert.AreEqual(ReferenceEquals(span.FontSize,clonedSpan.FontSize),false);
        }
    }
}
