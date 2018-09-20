/* 
*   Pedometer
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace PedometerU.Platforms {

    using AOT;
    using System.Runtime.InteropServices;

    public sealed class PedometeriOS : IPedometer {


        #region --IPedometer--

        event StepCallback IPedometer.OnStep {
            add {
                if (stepCallback == null) PedometerBridge.Initialize(OnStep);
                stepCallback += value;
            }
            remove {
                stepCallback -= value;
                if (stepCallback == null) PedometerBridge.Release();
            }
        }

        bool IPedometer.IsSupported {
            get {
                return PedometerBridge.IsSupported();
            }
        }
        #endregion


        #region --Operations--

        private StepCallback stepCallback;

        [MonoPInvokeCallback(typeof(StepCallback))]
        private static void OnStep (int steps, double distance) {
            // Relay
            PedometerUtility.Dispatch(() => {
                (Pedometer.Implementation as PedometeriOS).stepCallback(steps, distance);
            });
        }
        #endregion
    }
}