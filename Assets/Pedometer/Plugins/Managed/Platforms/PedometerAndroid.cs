/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU.Platforms {

    using UnityEngine;
    using Utilities;

    public sealed class PedometerAndroid : IPedometer { // INCOMPLETE

        #region --Properties--

        public event StepCallback OnStep {
            add {
                PedometerHelper.Instance.OnStep += value;
            } remove {
                PedometerHelper.Instance.OnStep -= value;
            }
        }
        
        public bool IsSupported {
            get {
                #if !UNITY_ANDROID || UNITY_EDITOR
                return false;
                #endif
                #pragma warning disable 0162
                // Get a reference to PedometerActivity
                pedometer = new AndroidJavaClass("com.yusufolokoba.pedometer.PedometerActivity");
                return false; // INCOMPLETE
                #pragma warning restore 0162
            }
        }
        #endregion


        #region --Op vars--
        private AndroidJavaClass pedometer;
        #endregion


        #region --Client API--

        public IPedometer Initialize () {
            // First check if IsSupported
            // Get reference to java classes?
            return this;
        }

        public void Release () {

        }
        #endregion
    }
}