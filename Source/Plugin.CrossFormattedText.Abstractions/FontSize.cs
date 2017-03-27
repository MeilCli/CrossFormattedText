using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossFormattedText.Abstractions {

    public struct FontSize {

        public static readonly FontSize UltraBig = new FontSize(1.5f);
        public static readonly FontSize Big = new FontSize(1.25f);
        public static readonly FontSize Normal = new FontSize(1f);
        public static readonly FontSize Small = new FontSize(0.75f);
        public static readonly FontSize UltraSmall = new FontSize(0.5f);

        public float Proportion { get; }

        public FontSize(float proportion) {
            Proportion = proportion;
        }
    }
}
