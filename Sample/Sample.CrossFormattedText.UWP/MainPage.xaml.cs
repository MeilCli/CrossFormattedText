﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Plugin.CrossFormattedText;
using Windows.UI.Xaml.Documents;
using Windows.UI;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace Sample.CrossFormattedText.UWP {
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();

            var paragraph = CrossCrossFormattedText.Current.Format(SpanText.HelloWorld).Span();

            TextBlock.Inlines.Clear();
            foreach(var span in paragraph.Inlines) {
                //TextBlock.Inlines.Add(span);
            }
            /*
            RichTextBlock.Inlines.Clear();
            foreach(var span in paragraph.Inlines) {
                if(span is Bold) {
                    continue;
                }
                RichTextBlock.Inlines.Add(span);
            }*/
            var pa = new Paragraph();
            pa.Inlines.Add(new Run { Text = "afaso" });
            foreach(var span in pa.Inlines) {
                TextBlock.Inlines.Add(span);
            }
        }
    }
}