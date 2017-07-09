using Android.App;
using Android.Widget;
using Android.OS;
using Sample.CrossFormattedText;
using Plugin.CrossFormattedText;
using System.Windows.Input;
using System;
using Plugin.CrossFormattedText.Abstractions;
using System.Collections.Generic;

namespace Sample.CrossFormattedText.Android {

    [Activity(Label = "Sample.CrossFormattedText.Android",MainLauncher = true,Icon = "@drawable/icon")]
    public class MainActivity : Activity {

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var textView = FindViewById<TextView>(Resource.Id.TextView);
            textView.TextFormatted = SpanText.HelloWorld.Build().Span();
            // for CommandableSpan method
            textView.SetTextWithCommandableSpan(SpanText.HelloWorld.Build());
        }
    }
}

