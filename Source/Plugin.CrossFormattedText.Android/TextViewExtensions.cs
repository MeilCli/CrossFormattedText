using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using Plugin.CrossFormattedText.Abstractions;

namespace Plugin.CrossFormattedText {
    public static class TextViewExtensions {

        public static void SetTextWithCommandableSpan(this TextView textView,ISpannableString spanableString) {
            textView.MovementMethod = ExtendedLinkMovementMethod.Instance;
            textView.TextFormatted = spanableString.Span();
            textView.Clickable = false;
            textView.LongClickable = false;
            if(Build.VERSION.SdkInt >= BuildVersionCodes.M) {
                textView.ContextClickable = false;
            }
        }
    }
}