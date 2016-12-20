using Plugin.CrossFormattedText.Abstractions;
using System;
using Windows.UI.Xaml.Documents;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;

namespace Plugin.CrossFormattedText
{

    public class CrossFormattedTextImplementation : ICrossFormattedText {

        public ISpannableString Format(FormattedString formattedString) {
            var sb = new Paragraph();

            foreach(var span in formattedString.Spans) {
                var run = new Run() {
                    Text = span.Text
                };
                Bold bold = null;
                if(span.ForegroundColor != null) {
                    run.Foreground = new SolidColorBrush(toColor(span.ForegroundColor));
                }
                if(span.BackgroundColor != null) {
                    // not find method
                }
                if(span.FontAttributes == FontAttributes.Bold) {
                    bold = new Bold();
                    bold.Inlines.Add(run);
                }
                if(span.FontAttributes == FontAttributes.Italic) {
                    run.FontStyle = FontStyle.Italic;
                }
                if(span.FontAttributes == (FontAttributes.Bold | FontAttributes.Italic)) {
                    bold = new Bold();
                    bold.Inlines.Add(run);
                    bold.FontStyle = FontStyle.Italic;
                }
                if(bold == null) {
                    sb.Inlines.Add(run);
                }else {
                    sb.Inlines.Add(bold);
                }
                
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