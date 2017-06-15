/* 
*   NatStep
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace NatStepU.Platforms {

    public sealed class NatStepLegacy : INatStep {

        #region --Properties--

        public event StepCallback OnStep;

        public bool IsSupported {get {return true;}}
        #endregion


        #region --Ctor--

        public NatStepLegacy () {
            // Do init stuff?
        }
        #endregion
    }
}