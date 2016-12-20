using Android.App;
using Android.Widget;
using Android.OS;
using Sample.CrossFormattedText;
using Plugin.CrossFormattedText;

namespace Sample.CrossFormattedText.Android {
    [Activity(Label = "Sample.CrossFormattedText.Android",MainLauncher = true,Icon = "@drawable/icon")]
    public class MainActivity : Activity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            var textView = FindViewById<TextView>(Resource.Id.TextView);
            textView.TextFormatted = CrossCrossFormattedText.Current.Format(SpanText.HelloWorld).Span();
        }
    }
}

