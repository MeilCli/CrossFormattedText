using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer;
using Plugin.CrossFormattedText;

namespace Test.CrossFormattedText.WindowsStore {

    [TestClass]
    public class Test {

        [UITestMethod]
        public void Pass() {
            CrossCrossFormattedText.Current.Format(SpanText.HelloWorld);
        }
    }
}
