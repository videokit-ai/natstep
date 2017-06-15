/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU.Platforms {

    public sealed class PedometerLegacy : IPedometer {

        #region --Properties--

        public event StepCallback OnStep;

        public bool IsSupported {get {return true;}}
        #endregion


        #region --Ctor--

        public PedometerLegacy () {
            // Do init stuff?
        }
        #endregion
    }
}