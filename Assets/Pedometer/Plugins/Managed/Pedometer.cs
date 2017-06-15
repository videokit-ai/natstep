/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU {

    using Platforms;
    using System.Linq;

    public sealed class Pedometer {

        #region --Op vars--
        private int? initial; // Some step counters count from device boot, so subtract the initial count we get
        private readonly StepCallback callback;
        public static readonly IPedometer Implementation;
        #endregion


        #region --Ctor--
        
        /// <summary>
        /// Create a new pedometer and start listening for updates
        /// </summary>
        public Pedometer (StepCallback callback) {
            // Register callback
            Implementation.OnStep += OnStep;
        }
        #endregion


        #region --Operations--

        /// <summary>
        /// Stop listening for pedometer updates
        /// </summary>
        public void Stop () {
            // Unregister callback
            Implementation.OnStep -= OnStep;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Release () {
            
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
            Implementation = new IPedometer[] {
                new PedometerAndroid(),
                new PedometeriOS(),
                new PedometerLegacy() // Always supported, uses GPS (so highly inaccurate)
            }.First(impl => impl.IsSupported);
        }
        #endregion
    }

    public delegate void StepCallback (int steps, double distance);
}