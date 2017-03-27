using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plugin.CrossFormattedText.Abstractions;

namespace Test.CrossFormattedText.Unit {
    
    [TestClass]
    public class FormattedStringTest {

        private static readonly FormattedString Text = new FormattedString(new Span[] {
            new Span() {
                Text = "This is sample text."
            },
            new Span() {
                Text = "\n"
            },
            new Span() {
                Text = "This is test text."
            },
            new Span() {
                Text = "\n"
            },
            new Span() {
                Text = "This is text."
            }
        });
        
        [TestMethod]
        public void ContainsTest() {
            Assert.AreEqual(Text.Contains(string.Empty),true);
            Assert.AreEqual(Text.Contains("This"),true);
            Assert.AreEqual(Text.Contains("test"),true);
            Assert.AreEqual(Text.Contains("\n"),true);
            Assert.AreEqual(Text.Contains("\nThis"),true);
            Assert.AreEqual(Text.Contains("That"),false);
            Assert.AreEqual(Text.Contains("was"),false);
            Assert.AreEqual(Text.Contains("text.net"),false);

            Span span1 = new Span() {
                Text = "This is text."
            };
            Span span2 = new Span() {
                Text = "That is text."
            };
            Assert.AreEqual(Text.Contains(span1),true);
            Assert.AreEqual(Text.Contains(span2),false);
        }
    }
}
