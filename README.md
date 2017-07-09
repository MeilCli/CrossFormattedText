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

*now document working*

- change FormattedString(finish)
  - string like operation(finish)
  - string like span operation(finish)
  - immutable text(finish)
  - muttable span meta data(finish)
  - encode/decode HTML like span text(beta finish)
  - implicit/explicit cast(finish)
- add FormattedStringBuilder(finish)
  - StringBuilder like operation(finish)
  - muttable text(finish)
  - immutable span meta data(finish)

## Document
### Span
Mutable text and mutable meta data

### EditableSpan
immutable text and mutable meta data

### FormattedStringBuilder
Alike StringBuilder, mutable text and immutable meta data.  
If change meta data, build FormattedString to access EditableSpan 

### FormattedString
Alike string, immutable text and mutable meta data.  
Two index type: text index or span index

- text index operator
  - string like operator
- span index operator
  - method name have suffix **Span
  - EditableSpan indexer

### FormattedStringConverter
Alike html converter.  
Not support Command, now.

The following syntax:
```
<span font-size="{enum name or value} font="{enum name}" f-color="{value (format: a,r,g,b)} b-color="{value (format: a,r,g,b)}">text</span>
```

The following enum name:
- font-size
  - ultra-big
  - big
  - normal
  - small
  - ultra-small
- font
  - none
  - bold
  - italic
  - italic-bold

## Usgae

In PCL: prepare FormattedString
```csharp
FormattedString HelloWorld = new FormattedString(new Span[] {
  new Span() {
    Text = "Hello",
    ForegroundColor = new SpanColor(125,125,125)
  },
  new Span() {
    Text = "World",
    FontAttributes = FontAttributes.Bold | FontAttributes.Italic,
    FontSize = FontSize.Small
  }
});
```

In PCL or target Platform: make ISpaanableString
```csharp
ISpannableString spannableString = HelloWorld.Build();
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
