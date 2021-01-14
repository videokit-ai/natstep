/* 
*   NatStep
*   Copyright (c) 2021 Yusuf Olokoba.
*/

namespace NatSuite.Sensors {

    using AOT;
    using System;
    using System.Runtime.InteropServices;
    using Internal;

    /// <summary>
    /// Step counter.
    /// </summary>
    public sealed class StepCounter : IDisposable {

        #region --Client API--
        /// <summary>
        /// Is step counting available on this device?
        /// </summary>
        public static bool Available => Bridge.Available();

        /// <summary>
        /// Create a step counter.
        /// </summary>
        /// <param name="handler">Delegate to receive step events.</param>
        public StepCounter (StepCountDelegate handler) {
            this.handle = GCHandle.Alloc(handler, GCHandleType.Normal);
            this.stepCounter = Bridge.CreatePedometer(OnStep, (IntPtr)handle);
        }

        /// <summary>
        /// Dispose the step counter and release resources
        /// </summary>
        public void Dispose () {
            handle.Free();
            stepCounter.Dispose();
        }
        #endregion


        #region --Operations--

        private readonly GCHandle handle;
        private readonly IntPtr stepCounter;

        [MonoPInvokeCallback(typeof(Bridge.StepHandler))]
        private static void OnStep (IntPtr context, int steps, long timestamp) => (((GCHandle)context).Target as Action<int, long>)?.Invoke(steps, timestamp);
        #endregion
    }
    
    /// <summary>
    /// Delegate invoked when a new step has been detected.
    /// </summary>
    /// <param name="steps">Aggregate number of steps detected by the step counter.</param>
    /// <param name="timestamp">Timestamo of the detected step event in nanoseconds.</param>
    public delegate void StepCountDelegate (int steps, long timestamp);
}