using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.CrossFormattedText.Abstractions;

namespace Plugin.CrossFormattedText {

    public static class FormattedStringExtensions {

        public static ISpannableString Build(this FormattedString formattedString) {
            return CrossCrossFormattedText.Current.Format(formattedString);
        }
    }
}
