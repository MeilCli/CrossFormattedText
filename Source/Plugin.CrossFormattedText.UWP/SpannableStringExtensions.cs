using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.CrossFormattedText.Abstractions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;

namespace Plugin.CrossFormattedText {
    public static class SpannableStringExtensions {

        public static List<Inline> Span(this ISpannableString spannableString) {
            if(spannableString is SpannableString) {
                return (spannableString as SpannableString).Text;
            }
            throw new NotSupportedException();
        }

        public static void SetTo(this ISpannableString spannableString,TextBlock textBlock) {
            textBlock.Inlines.Clear();
            foreach(var span in spannableString.Span()) {
                textBlock.Inlines.Add(span);
            }
        }

    }

}
