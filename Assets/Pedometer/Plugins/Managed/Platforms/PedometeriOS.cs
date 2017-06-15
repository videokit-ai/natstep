/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU.Platforms {

    using System.Runtime.InteropServices;
    using Utilities;

    public sealed class PedometeriOS : IPedometer {

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
                #if !UNITY_IOS || UNITY_EDITOR
                return false;
                #endif
                #pragma warning disable 0162
                return PDIsSupported();
                #pragma warning restore 0162
            }
        }
        #endregion


        #region --Client API--

        public IPedometer Initialize () {
            // Initialize pedometer natively
            PDInitialize();
            return this;
        }

        public void Release () {
            // Release pedometer natively
            PDRelease();
        }
        #endregion


        #region --Bridge--

        #if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void PDInitialize ();
        [DllImport("__Internal")]
        private static extern void PDRelease ();
        [DllImport("__Internal")]
        private static extern bool PDIsSupported ();

        #else // Keep IL2CPP happy
        private static void PDInitialize () {}
        private static void PDRelease () {}
        private static bool PDIsSupported () {return false;}
        #endif
        #endregion
    }
}