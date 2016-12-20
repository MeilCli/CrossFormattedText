using Plugin.CrossFormattedText.Abstractions;
using System;

namespace Plugin.CrossFormattedText
{
  /// <summary>
  /// Cross platform CrossFormattedText implemenations
  /// </summary>
  public class CrossCrossFormattedText
  {
    static Lazy<ICrossFormattedText> Implementation = new Lazy<ICrossFormattedText>(() => CreateCrossFormattedText(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use
    /// </summary>
    public static ICrossFormattedText Current
    {
      get
      {
        var ret = Implementation.Value;
        if (ret == null)
        {
          throw NotImplementedInReferenceAssembly();
        }
        return ret;
      }
    }

    static ICrossFormattedText CreateCrossFormattedText()
    {
#if PORTABLE
        return null;
#else
        return new CrossFormattedTextImplementation();
#endif
    }

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
