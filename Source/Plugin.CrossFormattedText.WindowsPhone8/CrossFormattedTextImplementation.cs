using Plugin.CrossFormattedText.Abstractions;
using System;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;

namespace Plugin.CrossFormattedText {

    public class CrossFormattedTextImplementation : ICrossFormattedText {

        public ISpannableString Format(FormattedString formattedString) {
            var sb = new List<Inline>();

            foreach(var span in formattedString.Spans) {
                var run = new Run() {
                    Text = span.Text
                };
                if(span.ForegroundColor != null) {
                    run.Foreground = new SolidColorBrush(toColor(span.ForegroundColor));
                }
                if(span.BackgroundColor != null) {
                    // not find method
                }
                if(span.FontSize != null) {
                    run.FontSize = run.FontSize * span.FontSize.Proportion;
                }
                if(span.FontAttributes == FontAttributes.Bold) {
                    run.FontWeight = FontWeights.Bold;
                }
                if(span.FontAttributes == FontAttributes.Italic) {
                    run.FontStyle = FontStyles.Italic;
                }
                if(span.FontAttributes == (FontAttributes.Bold | FontAttributes.Italic)) {
                    run.FontWeight = FontWeights.Bold;
                    run.FontStyle = FontStyles.Italic;
                }
                sb.Add(run);
            }

            return new SpannableString() {
                Text = sb
            };
        }

        private Color toColor(SpanColor spanColor) {
            return Color.FromArgb((byte)spanColor.Alpha,(byte)spanColor.Red,(byte)spanColor.Green,(byte)spanColor.Blue);
        }
    }
}