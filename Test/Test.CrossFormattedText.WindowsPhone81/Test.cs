using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Plugin.CrossFormattedText;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Test.CrossFormattedText.WindowsPhone81 {

    [TestClass]
    public class Test {

        [UITestMethod]
        [TestMethod]
        public void Pass() {
            CrossCrossFormattedText.Current.Format(SpanText.HelloWorld);
        }

        [AttributeUsage(AttributeTargets.Method,AllowMultiple = false)]
        public class UITestMethodAttribute : TestMethodAttribute {
            public override TestResult[] Execute(ITestMethod testMethod) {
                var task = ExecuteOnUi(testMethod);

                task.Wait();

                return task.Result;
            }

            private async Task<TestResult[]> ExecuteOnUi(ITestMethod testMethod) {
                var tsc = new TaskCompletionSource<TestResult[]>();

                await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,() => {
                    tsc.SetResult(base.Execute(testMethod));
                });

                return tsc.Task.Result;
            }
        }
    }
}
