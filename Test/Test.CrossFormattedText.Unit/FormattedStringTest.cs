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
            Span span1 = new Span() {
                Text = "This is text."
            };
            Span span2 = new Span() {
                Text = "That is text."
            };
            Assert.AreEqual(Text.Contains(span1),true);
            Assert.AreEqual(Text.Contains(span2),false);
        }

        [TestMethod]
        public void EndsWithTest() {
            Span span1 = new Span() {
                Text = "This is text."
            };
            Span span2 = new Span() {
                Text = "That is text."
            };
            Assert.AreEqual(Text.EndsWith(span1),true);
            Assert.AreEqual(Text.EndsWith(span2),false);
        }

        [TestMethod]
        public void IndexOfTest() {
            Span span1 = new Span() {
                Text = "This is sample text."
            };
            Span span2 = new Span() {
                Text = "This is text."
            };
            Span span3 = new Span() {
                Text = "That is text."
            };
            Assert.AreEqual(Text.IndexOf(span1),0);
            Assert.AreEqual(Text.IndexOf(span2),4);
            Assert.AreEqual(Text.IndexOf(span1,1),-1);
            Assert.AreEqual(Text.IndexOf(span2,0,1),-1);
            Assert.AreEqual(Text.IndexOf(span3),-1);
        }

        [TestMethod]
        public void InsertTest() {
            string s = " That is text.";

            var newText1 = Text.Insert(20,s);
            var newString1 = Text.PlainText.Insert(20,s);
            var newText2 = Text.Insert(20,s,SpanOperand.Left);
            var newString2 = Text.PlainText.Insert(20,s);
            Assert.AreEqual(newText1[1].Text.StartsWith(s),true);
            Assert.AreEqual(newText2[0].Text.EndsWith(s),true);
            Assert.AreEqual(newText1.Length,Text.Length);
            Assert.AreEqual(newText2.Length,Text.Length);
            Assert.AreEqual(newText1.PlainText,newString1);
            Assert.AreEqual(newText2.PlainText,newString2);
            Assert.AreEqual(Text.AnySpanReferenceEquals(newText1),false);
            Assert.AreEqual(Text.AnySpanReferenceEquals(newText2),false);

            Span span = new Span() {
                Text = s
            };
            var newText3 = Text.Insert(1,span);
            Assert.AreEqual(newText3[1].Equals(span),true);
            Assert.AreEqual(newText3.Length,Text.Length + 1);
        }

        [TestMethod]
        public void LastIndexOfTest() {
            Span span1 = new Span() {
                Text = "This is sample text."
            };
            Span span2 = new Span() {
                Text = "This is text."
            };
            Span span3 = new Span() {
                Text = "That is text."
            };
            Assert.AreEqual(Text.LastIndexOf(span1),0);
            Assert.AreEqual(Text.LastIndexOf(span2),4);
            Assert.AreEqual(Text.LastIndexOf(span1,1),0);
            Assert.AreEqual(Text.LastIndexOf(span2,1,1),-1);
            Assert.AreEqual(Text.LastIndexOf(span3),-1);
        }

        [TestMethod]
        public void RemoveTest() {
            var newText1 = Text.Remove(1);
            var newText2 = Text.Remove(1,Text.PlainText.Length-2);

            Assert.AreEqual(newText1.PlainText.Length,1);
            Assert.AreEqual(newText2.PlainText.Length,2);
            Assert.AreEqual(newText1.PlainText,"T");
            Assert.AreEqual(newText2.PlainText,"T.");
            Assert.AreEqual(newText1.AnySpanReferenceEquals(Text),false);
            Assert.AreEqual(newText2.AnySpanReferenceEquals(Text),false);
        }
    }
}
