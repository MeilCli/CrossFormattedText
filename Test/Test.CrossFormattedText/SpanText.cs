using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.CrossFormattedText.Abstractions;

namespace Test.CrossFormattedText {
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
                },
                new Span() {
                    Text = "\n"
                },
                new Span() {
                    Text = string.Empty
                },
                new Span() {
                    Text = "あああ",
                    FontAttributes = FontAttributes.Bold
                },
                new Span() {
                    Text = "aaa",
                    FontAttributes=FontAttributes.Italic,
                    BackgroundColor = new SpanColor(75,75,75),
                    ForegroundColor = new SpanColor(245,245,245)
                }
            }
        };
    }
}
