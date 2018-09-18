/* 
*   Pedometer
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace PedometerU.Platforms {

    public interface IPedometer {
        
        #region --Properties--
        /// <summary>
        /// Event used to propagate step events
        /// </summary>
        event StepCallback OnStep;
        /// <summary>
        /// Does the current device have a step counter?
        /// </summary>
        bool IsSupported { get; }
        #endregion
    }
}