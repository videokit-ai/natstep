/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU {

    using Platforms;
    using System;
    using System.Linq;

    public sealed class Pedometer : IDisposable {

        #region --Properties--

        /// <summary>
        /// Pedometer implementation for the current device. Do not use unless you know what you are doing
        /// </summary>
        public static IPedometer Implementation {
            get {
                return _Implementation = _Implementation ?? new IPedometer[] {
                    new PedometerAndroid(),
                    new PedometeriOS(),
                    new PedometerGPS() // Always supported, uses GPS (so highly inaccurate)
                }.First(impl => impl.IsSupported).Initialize();
            }
        }
        private static IPedometer _Implementation;
        #endregion

        #region --Op vars--
        private int? initial; // Some step counters count from device boot, so subtract the initial count we get
        private readonly StepCallback callback;
        #endregion


        #region --Ctor--
        
        /// <summary>
        /// Create a new pedometer and start listening for updates
        /// </summary>
        public Pedometer (StepCallback callback) {
            // Save the callback
            this.callback = callback;
            // Register callback
            Implementation.OnStep += OnStep;
        }
        #endregion


        #region --Operations--

        /// <summary>
        /// Stop listening for pedometer updates and dispose the object
        /// </summary>
        public void Dispose () {
            // Unregister callback
            Implementation.OnStep -= OnStep;
        }

        /// <summary>
        /// Release Pedometer and all of its resources
        /// </summary>
        public static void Release () {
            if (_Implementation == null) return;
            // Release and dereference
            _Implementation.Release();
            _Implementation = null;
        }

        private void OnStep (int steps, double distance) {
            // Set initial
            initial = initial ?? steps;
            // If this is not the first step, then invoke the callback
            if (steps > initial) if (callback != null) callback(steps, distance);
        }
        #endregion
    }

    /// <summary>
    /// A delegate used to pass pedometer information
    /// </summary>
    /// <param name="steps">Number of steps taken</param>
    /// <param name="distance">Distance walked in meters</param>
    public delegate void StepCallback (int steps, double distance);
}