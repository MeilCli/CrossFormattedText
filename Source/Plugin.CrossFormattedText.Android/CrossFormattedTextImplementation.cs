using Plugin.CrossFormattedText.Abstractions;
using System;
using Android.Graphics;

namespace Plugin.CrossFormattedText {

    public class CrossFormattedTextImplementation : ICrossFormattedText {

        public ISpannableString Format(FormattedString formattedString) {
            var sb = new Android.Text.SpannableStringBuilder();

            int startIndex = 0;
            int endIndex;
            foreach(var span in formattedString.Spans) {
                sb.Append(span.Text);
                endIndex = startIndex + span.Text.Length;

                if(span.ForegroundColor != SpanColor.DefaultValue) {
                    sb.SetSpan(
                        new Android.Text.Style.ForegroundColorSpan(toColor(span.ForegroundColor)),
                        startIndex,
                        endIndex,
                        Android.Text.SpanTypes.ExclusiveExclusive
                    );
                }
                if(span.BackgroundColor != SpanColor.DefaultValue) {
                    sb.SetSpan(
                        new Android.Text.Style.BackgroundColorSpan(toColor(span.ForegroundColor)),
                        startIndex,
                        endIndex,
                        Android.Text.SpanTypes.ExclusiveExclusive
                    );
                }
                if(span.FontSize != FontSize.Normal) {
                    sb.SetSpan(
                        new Android.Text.Style.RelativeSizeSpan(span.FontSize.Proportion),
                        startIndex,
                        endIndex,
                        Android.Text.SpanTypes.ExclusiveExclusive
                    );
                }
                if(span.FontAttributes == FontAttributes.Bold) {
                    sb.SetSpan(
                        new Android.Text.Style.StyleSpan(TypefaceStyle.Bold),
                        startIndex,
                        endIndex,
                        Android.Text.SpanTypes.ExclusiveExclusive
                    );
                } else if(span.FontAttributes == FontAttributes.Italic) {
                    sb.SetSpan(
                        new Android.Text.Style.StyleSpan(TypefaceStyle.Italic),
                        startIndex,
                        endIndex,
                        Android.Text.SpanTypes.ExclusiveExclusive
                    );
                } else if(span.FontAttributes == (FontAttributes.Bold | FontAttributes.Italic)) {
                    sb.SetSpan(
                        new Android.Text.Style.StyleSpan(TypefaceStyle.BoldItalic),
                        startIndex,
                        endIndex,
                        Android.Text.SpanTypes.ExclusiveExclusive
                    );
                }
                if(span.Command != null && span.CommandParameter != null) {
                    sb.SetSpan(
                        new CommandableSpan(span.Command,span.CommandParameter),
                        startIndex,
                        endIndex,
                        Android.Text.SpanTypes.ExclusiveExclusive
                    );
                }

                startIndex = endIndex;
            }

            return new SpannableString() {
                Text = sb
            };
        }

        private Color toColor(SpanColor spanColor) {
            return Color.Argb(spanColor.Alpha,spanColor.Red,spanColor.Green,spanColor.Blue);
        }
    }

}