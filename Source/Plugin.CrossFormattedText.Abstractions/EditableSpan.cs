using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plugin.CrossFormattedText.Abstractions {

    public class EditableSpan {

        private Span span;

        public string Text => span.Text;

        public SpanColor BackgroundColor {
            get {
                return span.BackgroundColor;
            }
            set {
                span.BackgroundColor = value;
            }
        }

        public SpanColor ForegroundColor {
            get {
                return span.ForegroundColor;
            }
            set {
                span.ForegroundColor = value;
            }
        }

        public FontAttributes FontAttributes {
            get {
                return span.FontAttributes;
            }
            set {
                span.FontAttributes = value;
            }
        }

        public FontSize FontSize {
            get {
                return span.FontSize;
            }
            set {
                span.FontSize = value;
            }
        }

        public ICommand Command {
            get {
                return span.Command;
            }
            set {
                span.Command = value;
            }
        }

        /// <summary>
        /// If use Command, require this
        /// </summary>
        public object CommandParameter {
            get {
                return span.CommandParameter;
            }
            set {
                span.CommandParameter = value;
            }
        }

        internal EditableSpan(Span span) {
            this.span = span;
        }

        public bool Equals(Span span) {
            return span.Equals(span);
        }
    }
}
