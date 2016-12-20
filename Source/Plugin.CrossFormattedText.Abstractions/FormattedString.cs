using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossFormattedText.Abstractions {
    public class FormattedString {

        private List<Span> _spans;

        public List<Span> Spans {
            get {
                if(_spans == null) {
                    throw new ArgumentNullException(nameof(Spans));
                }
                return _spans;
            }
            set {
                _spans = value;
            }
        }
    }
}
