using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Plugin.CrossFormattedText.Abstractions;

namespace Plugin.CrossFormattedText {
    public class SpannableString : ISpannableString {

        public List<Inline> Text { get; internal set; }
    }
}
