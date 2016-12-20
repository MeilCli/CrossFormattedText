using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.CrossFormattedText.Abstractions;
using Windows.UI.Xaml.Documents;

namespace Plugin.CrossFormattedText {
    public static class SpannableStringExtensions {

        public static Paragraph Span(this ISpannableString spannableString) {
            if(spannableString is SpannableString) {
                return (spannableString as SpannableString).Text;
            }
            throw new NotSupportedException();
        }
    }
}
