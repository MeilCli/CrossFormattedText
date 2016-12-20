using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Plugin.CrossFormattedText.Abstractions;

namespace Plugin.CrossFormattedText {
    public class SpannableString : ISpannableString {

        public ICharSequence Text { get; internal set; }
    }
}