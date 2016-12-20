using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using Plugin.CrossFormattedText.Abstractions;

namespace Plugin.CrossFormattedText {
    public class SpannableString : ISpannableString {

        public NSAttributedString Text { get; internal set; }
    }
}