# CrossFormattedText
[![NuGet version](https://badge.fury.io/nu/Plugin.CrossFormattedText.svg)](https://www.nuget.org/packages/Plugin.CrossFormattedText/)  
      
This library rapped some platform span class
- Android: SpannableStringBuilder(to ICharSequence
- iOS: NSMutableAttributedString(to NSAttributedString
- UWP: List\<Inline\>
- Windows Phone 8: List\<Inline\>
- Windows Phone 8.1: List\<Inline\>
- Windows 8.1: List\<Inline\>

## Features

| Platform | Supported | BackgroundColor | ForegroundColor | Bold/Italic | RelativeFontSize | Command |
|:---|:---:|:---:|:---:|:--:|:--:|:--:|
| Android | Yes | ✓ | ✓ | ✓ | ✓ | ✓ |
| iOS | Yes | ✓ | ✓ | ✓ | | will support(please advice) |
| UWP | Yes | | ✓ | ✓ | ✓ | ✓ |
| Windows Phone 8| Yes | | ✓ | ✓ | ✓ | |
| Windows Phone 8.1| Yes | | ✓ | ✓ | ✓ | ✓ |
| Windows 8.1| Yes | | ✓ | ✓ | ✓ | ✓ |
| Mac | No | | | | | |

## Working(Next Version)

Do destructive changes!!

- change FormattedString(working)
  - string like operation(finish)
  - string like span operation(finish)
  - immutable text(finish)
  - muttable span meta data(finish)
  - encode/decode HTML like span text(working)
  - implicit/explicit cast(finish)
- add FormattedStringBuilder(finish)
  - StringBuilder like operation(finish)
  - muttable text(finish)
  - immutable span meta data(finish)

## Usgae

In PCL: prepare FormattedString
```csharp
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
```csharp
ISpannableString spannableString = CrossCrossFormattedText.Current.Format(HelloWorld);
```

In target Platform: put spanned string
```csharp
//for Android
textView.TextFormatted = spannableString.Span();
textView.SetTextWithCommandableSpan(spannableString); // for using Command
//for iOS
Label.AttributedText = spannableString.Span();
//for All Windows Platform
spannableString.SetTo(TextBlock);
```

Span Method is only cast ISpannableString→each platform`s SpannableString and take Text field  
SetTo Method is for All Windows Platform, auto clear and add all span to TextBlock-Text.

## License
under MIT License
