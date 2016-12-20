using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using Plugin.CrossFormattedText.Abstractions;

namespace Plugin.CrossFormattedText {
    public static class SpannableStringExtensions {

        public static NSAttributedString Span(this ISpannableString spannableString) {
            if(spannableString is SpannableString) {
                return (spannableString as SpannableString).Text;
            }
            throw new NotSupportedException();
        }
    }
}