/* 
*   Pedometer
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace PedometerU.Platforms {

    using UnityEngine;

    public sealed class PedometerAndroid : AndroidJavaProxy, IPedometer {

        #region --IPedometer--

        event StepCallback IPedometer.OnStep {
            add {
                if (stepCallback == null) pedometer.Call("initialize");
                stepCallback += value;
            }
            remove {
                stepCallback -= value;
                if (stepCallback == null) pedometer.Call("release");
            }
        }

        bool IPedometer.IsSupported {
            get {
                return pedometer.Call<bool>("isSupported");
            }
        }
        #endregion


        #region --Operations--

        private StepCallback stepCallback;
        private readonly AndroidJavaObject pedometer;

        public PedometerAndroid () : base("com.yusufolokoba.pedometer.PedometerDelegate") {
            pedometer = new AndroidJavaObject("com.yusufolokoba.pedometer.Pedometer", this);
        }

        private void onStep (int steps, double distance) {
            // Relay
            PedometerUtility.Dispatch(() => stepCallback(steps, distance));
        }
        #endregion
    }
}