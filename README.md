# CrossFormattedText
[![NuGet version](https://badge.fury.io/nu/Plugin.CrossFormattedText.svg)](https://www.nuget.org/packages/Plugin.CrossFormattedText/)  
-- Now Not Testing iOS--
      
This library rapped some platform span class
- Android: SpannableStringBuilder(to ICharSequence
- iOS: NSMutableAttributedString(to NSAttributedString
- UWP: List\<Inline\>
- Windows Phone 8: List\<Inline\>
- Windows Phone 8.1: List\<Inline\>
- Windows 8.1: List\<Inline\>

## Features

| Platform | Supported | BackgroundColor | ForegroundColor | Bold/Italic | RelativeFontSize |
|:---|:---:|:---:|:---:|:--:|:--:|
| Android | Yes | ✓ | ✓ | ✓ | ✓ |
| iOS | Yes(no test) | ✓ | ✓ | ✓ | |
| UWP | Yes | | ✓ | ✓ | ✓ |
| Windows Phone 8| Yes | | ✓ | ✓ | ✓ |
| Windows Phone 8.1| Yes | | ✓ | ✓ | ✓ |
| Windows 8.1| Yes | | ✓ | ✓ | ✓ |
| Mac | No | | | | |

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
//for All Windows Platform
spannableString.SetTo(TextBlock);
```

Span Method is only cast ISpannableString→each platform`s SpannableString and take Text field  
SetTo Method is for All Windows Platform, auto clear and add all span to TextBlock-Text.

## License
under MIT License
