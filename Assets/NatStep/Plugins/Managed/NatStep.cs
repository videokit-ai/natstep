/* 
*   NatStep
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace NatStepU {

    using Platforms;
    using System.Linq;

    public sealed class Pedometer {

        #region --Op vars--
        private int? initial; // Some step counters count from device boot, so subtract the initial count we get
        private readonly StepCallback callback;
        public static readonly INatStep Implementation;
        #endregion


        #region --Ctor--

        public Pedometer (StepCallback callback) {
            // Register callback
            Implementation.OnStep += OnStep;
        }
        #endregion


        #region --Operations--

        public void Release () {
            // Unregister callback
            Implementation.OnStep -= OnStep;
        }

        private void OnStep (int steps, double distance) {
            // Set initial
            initial = initial ?? steps;
            // If this is not the first step, then invoke the callback
            if (steps > initial) if (callback != null) callback(steps, distance);
        }
        #endregion


        #region --Initialization--

        static Pedometer () {
            // Create an implementation for this platform
            Implementation = new INatStep[] {
                new NatStepAndroid(),
                new NatStepiOS(),
                new NatStepLegacy() // Always supported, uses GPS (so highly inaccurate)
            }.First(impl => impl.IsSupported);
        }
        #endregion
    }

    public delegate void StepCallback (int steps, double distance);
}