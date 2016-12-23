using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer;
using Plugin.CrossFormattedText;

namespace Test.CrossFormattedText.UWP {

    [TestClass]
    public class Test {

        [UITestMethod]
        public void Pass() {
            CrossCrossFormattedText.Current.Format(SpanText.HelloWorld);
        }

    }
}
