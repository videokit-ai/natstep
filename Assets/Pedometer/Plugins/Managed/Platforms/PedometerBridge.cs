/* 
*   Pedometer
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace PedometerU.Platforms {

    using System.Runtime.InteropServices;

    public static class PedometerBridge {
        
        private const string Assembly =
        #if (UNITY_IOS || UNITY_WEBGL) && !UNITY_EDITOR
        "__Internal";
        #else
        "Pedometer";
        #endif

        #if UNITY_IOS
        [DllImport(Assembly, EntryPoint = "PDInitialize")]
        private static extern void Initialize (StepCallback callback);
        [DllImport(Assembly, EntryPoint = "PDRelease")]
        private static extern void Release ();
        [DllImport(Assembly, EntryPoint = "PDIsSupported")]
        private static extern bool IsSupported ();
        #else
        public static void Initialize (StepCallback callback) {}
        public static void Release () {}
        public static bool IsSupported () { return false; }
        #endif
    }
}