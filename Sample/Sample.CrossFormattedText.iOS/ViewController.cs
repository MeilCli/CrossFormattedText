using System;

using UIKit;
using Plugin.CrossFormattedText;

namespace Sample.CrossFormattedText.iOS {
    public partial class ViewController : UIViewController {
        public ViewController(IntPtr handle) : base(handle) {
        }

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            this.Label.AttributedText = SpanText.HelloWorld.Build().Span();
        }

        public override void DidReceiveMemoryWarning() {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}