/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU.Platforms {

    public interface IPedometer {
        
        #region --Properties--
        /// <summary>
        /// Event used to propagate step events
        /// </summary>
        event StepCallback OnStep;
        /// <summary>
        /// Is this implementation supported by the current platform?
        /// </summary>
        bool IsSupported {get;}
        #endregion
    }
}