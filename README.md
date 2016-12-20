# CrossFormattedText
-- Now Tested Only Android --
      
This library rapped some platform span class
- Android: SpannableStringBuilder(to ICharSequence
- iOS: NSMutableAttributedString(to NSAttributedString
- UWP: Paragraph
      
coming soon(Windows Phone,Windows RT

## Attension
UWP not support background color(I don`t find method)

## Usgae

In PCL: prepare FormattedString
```
FormattedString HelloWorld = new FormattedString() {
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
```

In PCL or target Platform: make ISpaanableString
```
ISpannableString spannableString = CrossCrossFormattedText.Current.Format(HelloWorld);
```

In target Platform: put spanned string
```
//for Android
textView.TextFormatted = spannableString.Span();
```

Span Method is only cast ISpannableString→each platform`s SpannableString and take Text field

## License
under MIT License
