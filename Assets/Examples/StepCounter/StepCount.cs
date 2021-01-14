/* 
*   NatStep
*   Copyright (c) 2021 Yusuf Olokoba
*/

namespace NatSuite.Examples {

    using UnityEngine;
    using UnityEngine.UI;
    using Sensors;

    public class StepCount : MonoBehaviour {

        public Text stepText;
        private StepCounter stepCounter;

        private void Start () => stepCounter = new StepCounter(OnStep);

        private void OnStep (int steps, long timestamp) => stepText.text = steps.ToString();

        private void OnDisable () => stepCounter.Dispose();
    }
}