using Plugin.CrossFormattedText.Abstractions;
using System;
using Windows.UI;
using System.Collections.Generic;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Text;
using Windows.UI.Xaml;
using System.Windows.Input;

namespace Plugin.CrossFormattedText {

    public class CrossFormattedTextImplementation : ICrossFormattedText {

        private static readonly DependencyProperty commandProperty = DependencyProperty.Register("Command",typeof(ICommand),typeof(Hyperlink),new PropertyMetadata(null));
        private static readonly DependencyProperty commandParameterProperty = DependencyProperty.Register("CommandParameter",typeof(object),typeof(Hyperlink),new PropertyMetadata(null));

        public ISpannableString Format(FormattedString formattedString) {
            var sb = new List<Inline>();

            foreach(var span in formattedString.Spans) {
                var run = new Run() {
                    Text = span.Text
                };
                if(span.ForegroundColor != SpanColor.DefaultValue) {
                    run.Foreground = new SolidColorBrush(toColor(span.ForegroundColor));
                }
                if(span.BackgroundColor != SpanColor.DefaultValue) {
                    // not find method
                }
                if(span.FontSize != FontSize.Normal) {
                    run.FontSize = run.FontSize * span.FontSize.Proportion;
                }
                if(span.FontAttributes == FontAttributes.Bold) {
                    run.FontWeight = FontWeights.Bold;
                }
                if(span.FontAttributes == FontAttributes.Italic) {
                    run.FontStyle = FontStyle.Italic;
                }
                if(span.FontAttributes == (FontAttributes.Bold | FontAttributes.Italic)) {
                    run.FontWeight = FontWeights.Bold;
                    run.FontStyle = FontStyle.Italic;
                }

                if(span.Command != null && span.CommandParameter != null) {
                    var link = new Hyperlink();
                    link.Inlines.Add(run);
                    link.Click += hyperLinkClicked;
                    link.SetValue(commandProperty,span.Command);
                    link.SetValue(commandParameterProperty,span.CommandParameter);
                    sb.Add(link);
                    continue;
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

        private static void hyperLinkClicked(object sender,HyperlinkClickEventArgs args) {
            var command = (sender as Hyperlink).GetValue(commandProperty) as ICommand;
            var parameter = (sender as Hyperlink).GetValue(commandParameterProperty);
            if(command.CanExecute(parameter) == false) {
                return;
            }
            command.Execute(parameter);
        }
    }
}