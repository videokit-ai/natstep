/* 
*   NatStep
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace NatStepU.Platforms {

    public sealed class NatStepiOS : INatStep {

        #region --Properties--

        public event StepCallback OnStep;

        public bool IsSupported {
            get {
                #if !UNITY_IOS || UNITY_EDITOR
                return false;
                #endif
                #pragma warning disable 0162
                return false; // INCOMPLETE
                #pragma warning restore 0162
            }
        }
        #endregion


        #region --Ctor--

        public NatStepiOS () {
            // Do init stuff?
        }
        #endregion
    }
}