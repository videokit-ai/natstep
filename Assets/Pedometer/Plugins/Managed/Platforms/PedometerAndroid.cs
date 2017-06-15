/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU.Platforms {

    public sealed class PedometerAndroid : IPedometer {

        #region --Properties--

        public event StepCallback OnStep;
        
        public bool IsSupported {
            get {
                #if !UNITY_ANDROID || UNITY_EDITOR
                return false;
                #endif
                #pragma warning disable 0162
                return false; // INCOMPLETE
                #pragma warning restore 0162
            }
        }
        #endregion


        #region --Ctor--

        public PedometerAndroid () {
            // First check if IsSupported
            // Get reference to java classes?
        }
        #endregion
    }
}