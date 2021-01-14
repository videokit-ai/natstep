/* 
*   NatStep
*   Copyright (c) 2021 Yusuf Olokoba.
*/

namespace NatSuite.Sensors.Internal {

    using System;
    using System.Runtime.InteropServices;
    
    public static class Bridge {

        private const string Assembly =
        #if UNITY_IOS && !UNITY_EDITOR
        @"__Internal";
        #else
        @"NatStep";
        #endif

        public delegate void StepHandler (IntPtr context, int steps, long timestamp);

        [DllImport(Assembly, EntryPoint = @"NSStepCounterAvailable")]
        public static extern bool Available ();
        [DllImport(Assembly, EntryPoint = "NSCreateStepCounter")]
        public static extern IntPtr CreatePedometer (StepHandler handler, IntPtr context);
        [DllImport(Assembly, EntryPoint = @"NSDisposeStepCounter")]
        public static extern void Dispose (this IntPtr pedometer);
    }
}