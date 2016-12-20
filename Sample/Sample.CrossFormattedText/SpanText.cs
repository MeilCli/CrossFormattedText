using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.CrossFormattedText.Abstractions;

namespace Sample.CrossFormattedText {
    public static class SpanText {

        public static readonly FormattedString HelloWorld = new FormattedString() {
            Spans = new List<Span>() {
                new Span() {
                    Text = "Hellow",
                    ForegroundColor = new SpanColor(125,125,125)
                },
                new Span() {
                    Text = "World",
                    FontAttributes = FontAttributes.Bold | FontAttributes.Italic
                }
            }
        };
    }
}
