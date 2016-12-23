using System;
using NUnit.Framework;
using Plugin.CrossFormattedText;

namespace Test.CrossFormattedText.Android {
    [TestFixture]
    public class Test {

        [Test]
        public void Pass() {
            Assert.DoesNotThrow(() => CrossCrossFormattedText.Current.Format(SpanText.HelloWorld));
        }

    }
}