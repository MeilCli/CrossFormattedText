using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plugin.CrossFormattedText.Abstractions {

    public class Span {

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

    }

}
