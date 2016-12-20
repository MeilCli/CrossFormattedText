using System;

namespace Plugin.CrossFormattedText.Abstractions {
    public interface ICrossFormattedText {

        ISpannableString Format(FormattedString formattedString);
    }
}
