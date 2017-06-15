/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU.Platforms {

    using UnityEngine;
    using System.Collections;

    public sealed class PedometerLegacy : IPedometer {

        #region --Properties--

        public event StepCallback OnStep;

        public bool IsSupported {get {return true;}}
        #endregion


        #region --Client API--

        public IPedometer Initialize () {
            return this;
        }

        public void Release () {

        }
        #endregion


        #region --Operations--

        #endregion
    }
}