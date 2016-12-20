using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.CrossFormattedText.Abstractions;
using Windows.UI.Xaml.Documents;

namespace Plugin.CrossFormattedText {
    public class SpannableString : ISpannableString{

        public Paragraph Text { get; internal set; }
    }
}
