using Plugin.CrossFormattedText.Abstractions;
using System;
using Foundation;
using UIKit;

namespace Plugin.CrossFormattedText {
    public class CrossFormattedTextImplementation : ICrossFormattedText {

        public ISpannableString Format(FormattedString formattedString) {
            var sb = new NSMutableAttributedString();

            foreach(var span in formattedString) {
                if(span.ForegroundColor != null && span.BackgroundColor != null && (span.FontAttributes & FontAttributes.None) == FontAttributes.None) {
                    sb.Append(new NSAttributedString(span.Text));
                    continue;
                }
                var ui = new UIStringAttributes();
                if(span.ForegroundColor != SpanColor.DefaultValue) {
                    ui.ForegroundColor = toColor(span.ForegroundColor);
                }
                if(span.BackgroundColor != SpanColor.DefaultValue) {
                    ui.BackgroundColor = toColor(span.BackgroundColor);
                }
                if(span.FontAttributes == FontAttributes.Bold) {
                    ui.Font = UIFont.BoldSystemFontOfSize(UIFont.SystemFontSize);
                } else if(span.FontAttributes == FontAttributes.Italic) {
                    ui.Font = UIFont.ItalicSystemFontOfSize(UIFont.SystemFontSize);
                } else if(span.FontAttributes == (FontAttributes.Bold | FontAttributes.Italic)) {
                    var baseFontDescriptor = UIFont.SystemFontOfSize(UIFont.SystemFontSize).FontDescriptor;
                    ui.Font = UIFont.FromDescriptor(baseFontDescriptor.CreateWithTraits(UIFontDescriptorSymbolicTraits.Bold | UIFontDescriptorSymbolicTraits.Italic),0);
                }
                if(span.FontSize != FontSize.Normal) {
                    ui.Font = ui.Font.WithSize(ui.Font.PointSize * span.FontSize.Proportion);
                }
                sb.Append(new NSAttributedString(span.Text,ui));
            }

            return new SpannableString() {
                Text = sb
            };
        }

        private UIColor toColor(SpanColor spanColor) {
            return UIColor.FromRGBA(spanColor.Red,spanColor.Green,spanColor.Blue,spanColor.Alpha);
        }
    }
}