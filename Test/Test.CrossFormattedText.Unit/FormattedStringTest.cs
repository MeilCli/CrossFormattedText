using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plugin.CrossFormattedText.Abstractions;

namespace Test.CrossFormattedText.Unit {
    
    [TestClass]
    public class FormattedStringTest {

        private static readonly FormattedString text = new FormattedString(new Span[] {
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
            Assert.AreEqual(text.Contains(span1),true);
            Assert.AreEqual(text.Contains(span2),false);
        }

        [TestMethod]
        public void EndsWithTest() {
            Span span1 = new Span() {
                Text = "This is text."
            };
            Span span2 = new Span() {
                Text = "That is text."
            };
            Assert.AreEqual(text.EndsWith(span1),true);
            Assert.AreEqual(text.EndsWith(span2),false);
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
            Assert.AreEqual(text.IndexOf(span1),0);
            Assert.AreEqual(text.IndexOf(span2),4);
            Assert.AreEqual(text.IndexOf(span1,1),-1);
            Assert.AreEqual(text.IndexOf(span2,0,1),-1);
            Assert.AreEqual(text.IndexOf(span3),-1);
        }

        [TestMethod]
        public void InsertTest() {
            string s = " That is text.";

            var newText1 = text.Insert(20,s);
            var newString1 = text.Text.Insert(20,s);
            var newText2 = text.Insert(20,s,SpanOperand.Left);
            var newString2 = text.Text.Insert(20,s);
            Assert.AreEqual(newText1[1].Text.StartsWith(s),true);
            Assert.AreEqual(newText2[0].Text.EndsWith(s),true);
            Assert.AreEqual(newText1.Length,text.Length);
            Assert.AreEqual(newText2.Length,text.Length);
            Assert.AreEqual(newText1.Text,newString1);
            Assert.AreEqual(newText2.Text,newString2);
            Assert.AreEqual(text.AnySpanReferenceEquals(newText1),false);
            Assert.AreEqual(text.AnySpanReferenceEquals(newText2),false);

            Span span = new Span() {
                Text = s
            };
            var newText3 = text.Insert(1,span);
            Assert.AreEqual(newText3[1].Equals(span),true);
            Assert.AreEqual(newText3.Length,text.Length + 1);
            Assert.AreEqual(text.AnySpanReferenceEquals(newText3),false);
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
            Assert.AreEqual(text.LastIndexOf(span1),0);
            Assert.AreEqual(text.LastIndexOf(span2),4);
            Assert.AreEqual(text.LastIndexOf(span1,1),0);
            Assert.AreEqual(text.LastIndexOf(span2,1,1),-1);
            Assert.AreEqual(text.LastIndexOf(span3),-1);
        }

        [TestMethod]
        public void RemoveTest() {
            var newText1 = text.Remove(1);
            var newText2 = text.Remove(1,text.TextLength-2);

            Assert.AreEqual(newText1.TextLength,1);
            Assert.AreEqual(newText2.TextLength,2);
            Assert.AreEqual(newText1.Text,"T");
            Assert.AreEqual(newText2.Text,"T.");
            Assert.AreEqual(newText1.AnySpanReferenceEquals(text),false);
            Assert.AreEqual(newText2.AnySpanReferenceEquals(text),false);
        }

        [TestMethod]
        public void RemoveSpanTest() {
            var newText1 = text.RemoveSpan(1);
            var newText2 = text.RemoveSpan(1,text.Length - 2);

            Assert.AreEqual(newText1.Length,1);
            Assert.AreEqual(newText2.Length,2);
            Assert.AreEqual(newText1[0].Equals(text[0]),true);
            Assert.AreEqual(newText2[0].Equals(text[0]),true);
            Assert.AreEqual(newText2[1].Equals(text[text.Length - 1]),true);
            Assert.AreEqual(newText1.AnySpanReferenceEquals(text),false);
            Assert.AreEqual(newText2.AnySpanReferenceEquals(text),false);
        }
    }
}
