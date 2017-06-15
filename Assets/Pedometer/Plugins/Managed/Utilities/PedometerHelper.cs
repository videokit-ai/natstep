/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU.Utilities {

    using UnityEngine;
    using System.Collections;

    /// <summary>
    /// Helper class used for dispatching coroutines and receiving native events raised by UnitySendMessage
    /// </summary>
    public class PedometerHelper : MonoBehaviour {


        #region --Properties--
        public event StepCallback OnStep;
        public static readonly PedometerHelper Instance;
        #endregion


        #region --Client API--

        /// <summary>
        /// Dispatch a coroutine to be invoked
        /// </summary>
        /// <returns>The queued coroutine</returns>
        public Coroutine Dispatch (IEnumerator routine) {
            return StartCoroutine(routine);
        }

        /// <summary>
        /// Event used by native implementations to report pedometer events
        /// </summary>
        private void OnEvent (string data) {
            // Schema: "steps:distance"
            int steps; double distance; string[] tokens = data.Split(':');
            // Parse
            if (OnStep == null || !int.TryParse(tokens[0], out steps) || !double.TryParse(tokens[1], out distance)) return;
            // Raise event
            OnStep(steps, distance);
        }
        #endregion


        #region --Initialization--

        static PedometerHelper () {
            // Create the singleton
            Instance = new GameObject("Pedometer").AddComponent<PedometerHelper>();
        }

        private void Awake () {
            // Preserve across scenes
            DontDestroyOnLoad(this);
        }
        #endregion
    }
}