using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plugin.CrossFormattedText.Abstractions {

    public class Span : IEquatable<Span> {

        public string Text { get; set; } = string.Empty;

        public SpanColor BackgroundColor { get; set; } = SpanColor.DefaultValue;

        public SpanColor ForegroundColor { get; set; } = SpanColor.DefaultValue;

        public FontAttributes FontAttributes { get; set; } = FontAttributes.None;

        public FontSize FontSize { get; set; } = FontSize.Normal;

        public ICommand Command { get; set; }

        /// <summary>
        /// If use Command, require this
        /// </summary>
        public object CommandParameter { get; set; }

        public override bool Equals(object obj) {
            if(obj == null) {
                return false;
            }
            if(obj is Span) {
                return Equals((Span)obj);
            }
            return false;
        }

        public bool Equals(Span span) {
            if(span == null) {
                return false;
            }
            if(ReferenceEquals(this,span)) {
                return true;
            }
            if(Text != span.Text) {
                return false;
            }
            if(BackgroundColor != span.BackgroundColor) {
                return false;
            }
            if(ForegroundColor != span.ForegroundColor) {
                return false;
            }
            if(FontAttributes != span.FontAttributes) {
                return false;
            }
            if(FontSize != span.FontSize) {
                return false;
            }
            if(Command != span.Command) {
                return false;
            }
            if(CommandParameter != span.CommandParameter) {
                return false;
            }
            return true;
        }

        public override int GetHashCode() {
            int hash = Text?.GetHashCode() ?? -1;
            hash ^= BackgroundColor.GetHashCode();
            hash ^= ForegroundColor.GetHashCode();
            hash ^= FontAttributes.GetHashCode();
            hash ^= FontSize.GetHashCode();
            hash ^= Command?.GetHashCode() ?? -1;
            hash ^= CommandParameter?.GetHashCode() ?? -1;
            return hash;
        }

    }

}
