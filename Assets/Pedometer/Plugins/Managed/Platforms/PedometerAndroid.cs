/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU.Platforms {

    using UnityEngine;
    using Utilities;

    public sealed class PedometerAndroid : IPedometer {

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
                using (var player = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) pedometer = player.GetStatic<AndroidJavaObject>("currentActivity");
                // Check if supported
                return pedometer.Call<bool>("isSupported");
                #pragma warning restore 0162
            }
        }
        #endregion


        #region --Op vars--
        private AndroidJavaObject pedometer;
        #endregion


        #region --Client API--

        public IPedometer Initialize () {
            // Initialize pedometer natively
            pedometer.Call("initialize");
            return this;
        }

        public void Release () {
            // Release pedometer natively
            pedometer.Call("release");
        }
        #endregion
    }
}