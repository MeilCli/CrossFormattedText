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
            Assert.AreEqual(Text.Contains(Text.ToPlainText()),true);
            Assert.AreEqual(Text.Contains("That"),false);
            Assert.AreEqual(Text.Contains("was"),false);
            Assert.AreEqual(Text.Contains("text.net"),false);
            Assert.AreEqual(Text.Contains(Text.ToPlainText() + "aa"),false);

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
            Assert.AreEqual(Text.EndsWith(string.Empty),true);
            Assert.AreEqual(Text.EndsWith("text."),true);
            Assert.AreEqual(Text.EndsWith("."),true);
            Assert.AreEqual(Text.EndsWith("\nThis is text."),true);
            Assert.AreEqual(Text.EndsWith(Text.ToPlainText()),true);
            Assert.AreEqual(Text.EndsWith("test."),false);
            Assert.AreEqual(Text.EndsWith("aa" + Text.ToPlainText()),false);


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
            Assert.AreEqual(Text.IndexOf('T'),0);
            Assert.AreEqual(Text.IndexOf(' '),4);
            Assert.AreEqual(Text.IndexOf('<'),-1);
            Assert.AreEqual(Text.IndexOf(' ',5,3),7);
            Assert.AreEqual(Text.IndexOf(' ',5,2),-1);
            Assert.AreEqual(Text.IndexOf('.',Text.TextLength - 1),Text.TextLength - 1);

            Assert.AreEqual(Text.IndexOf("T"),0);
            Assert.AreEqual(Text.IndexOf("This "),0);
            Assert.AreEqual(Text.IndexOf("This ",1),21);
            Assert.AreEqual(Text.IndexOf("text.",Text.TextLength - 5),Text.TextLength - 5);
            Assert.AreEqual(Text.IndexOf("text.",Text.TextLength - 4),-1);
            Assert.AreEqual(Text.IndexOf("That"),-1);

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
            var newString1 = Text.ToPlainText().Insert(20,s);
            var newText2 = Text.Insert(20,s,SpanOperand.Left);
            var newString2 = Text.ToPlainText().Insert(20,s);
            Assert.AreEqual(newText1[1].Text.StartsWith(s),true);
            Assert.AreEqual(newText2[0].Text.EndsWith(s),true);
            Assert.AreEqual(newText1.Length,Text.Length);
            Assert.AreEqual(newText2.Length,Text.Length);
            Assert.AreEqual(newText1.ToPlainText(),newString1);
            Assert.AreEqual(newText2.ToPlainText(),newString2);
            Assert.AreEqual(Text.AnySpanReferenceEquals(newText1),false);
            Assert.AreEqual(Text.AnySpanReferenceEquals(newText2),false);

            Span span = new Span() {
                Text = s
            };
            var newText3 = Text.Insert(1,span);
            Assert.AreEqual(newText3[1],span);
            Assert.AreEqual(newText3.Length,Text.Length + 1);
        }
    }
}
