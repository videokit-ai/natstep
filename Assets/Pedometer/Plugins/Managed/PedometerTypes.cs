/* 
*   Pedometer
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace PedometerU {

    /// <summary>
    /// A delegate used to pass pedometer information
    /// </summary>
    /// <param name="steps">Number of steps taken</param>
    /// <param name="distance">Distance walked in meters</param>
    public delegate void StepCallback (int steps, double distance);
}