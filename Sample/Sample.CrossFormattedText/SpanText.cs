using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
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
                    FontAttributes = FontAttributes.Bold | FontAttributes.Italic,
                    FontSize = FontSize.Small
                },
                new Span() {
                    Text = "Clickable!",
                    ForegroundColor = new SpanColor(60,90,170),
                    Command = new DebugCommand(),
                    CommandParameter = "Clicked",
                }
            }
        };
    }

    class DebugCommand : ICommand {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            System.Diagnostics.Debug.WriteLine(parameter);
        }
    }
}
