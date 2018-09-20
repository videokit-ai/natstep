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
        public static extern void Initialize (StepCallback callback);
        [DllImport(Assembly, EntryPoint = "PDRelease")]
        public static extern void Release ();
        [DllImport(Assembly, EntryPoint = "PDIsSupported")]
        public static extern bool IsSupported ();
        #else
        public static void Initialize (StepCallback callback) {}
        public static void Release () {}
        public static bool IsSupported () { return false; }
        #endif
    }
}