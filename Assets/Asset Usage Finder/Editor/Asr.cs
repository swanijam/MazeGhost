using System.Diagnostics;
using UnityEngine;

namespace AssetUsageFinder {
    static class FLAGS {
        public const string DEBUG = "DEBUG1"; //todo rename in release
    }

    static class Asr {
#line hidden
        [Conditional(FLAGS.DEBUG)]
        public static void AreEqual(int a, int b) {
            UnityEngine.Assertions.Assert.AreEqual(a, b);
        }

        [Conditional(FLAGS.DEBUG)]
        public static void IsTrue(bool b, string format = null) {
            UnityEngine.Assertions.Assert.IsTrue(b, format);
        }

        [Conditional(FLAGS.DEBUG)]
        public static void IsNotNull(Object target, string assetYouReTryingToSearchIsCorrupted) {
            UnityEngine.Assertions.Assert.IsNotNull(target, assetYouReTryingToSearchIsCorrupted);
        }
#line default
    }
}