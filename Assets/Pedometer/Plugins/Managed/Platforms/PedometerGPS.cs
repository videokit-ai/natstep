/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU.Platforms {

    using UnityEngine;
    using System;
    using System.Collections;
    using Utilities;

    public sealed class PedometerGPS : IPedometer {

        #region --Properties--
        public event StepCallback OnStep;
        public bool IsSupported {get {return true;}}
        public bool Running {get; private set;}
        private const double
        LocationTimeout = 20f,
        EarthRadius = 6378.137d, // Meters
        DegreesToRadians = Math.PI / 180d,
        StepsToMeters = 0.715f;
        #endregion


        #region --Client API--

        public IPedometer Initialize () {
            // Check that the user has granted permissions
            if (!Input.location.isEnabledByUser) Debug.LogError("Pedometer Error: GPS backend requires GPS permissions");
            // Initialize location services
            else PedometerHelper.Instance.Dispatch(Update());
            // Log
            Debug.Log("Pedometer: Initialized GPS backend");
            return this;
        }

        public void Release () {
            // Stop running
            Running = false;
            // Log
            Debug.Log("Pedometer: Released GPS backend");
        }
        #endregion


        #region --Operations--

        private IEnumerator Update () {
            // Start location service
            Input.location.Start(0.5f, 3f);
            // Wait until service initializes
            var time = Time.realtimeSinceStartup;
            yield return new WaitWhile(() => Input.location.status == LocationServiceStatus.Initializing && Time.realtimeSinceStartup - time < LocationTimeout);
            // Check that service initialized
            if (Input.location.status != LocationServiceStatus.Running) {
                Debug.LogError("Pedometer Error: Failed to initialize location service with status: "+Input.location.status);
                yield break;
            }
            // Get current location
            var lastData = Input.location.lastData;
            var distance = 0.0d;
            Running = true;
            // Update location
            while (Running) {
                // State checking
                if (Input.location.status != LocationServiceStatus.Running) yield break;
                // Check that the data was updated
                if (Math.Abs(Input.location.lastData.timestamp - lastData.timestamp) < 1e-10) goto end;
                // Accumulate haversine distance
                var currentData = Input.location.lastData;
                distance += Distance(lastData.latitude, lastData.longitude, currentData.latitude, currentData.longitude);
                // Invoke event
                if (OnStep != null) OnStep((int)(distance / StepsToMeters), distance);
                // Update data
                lastData = currentData;
                // Next frame
                end: yield return null;
            }
            // Stop location service
            Input.location.Stop();
        }
        
        /// <summary>
        /// Calculate the haversine distance between two latitude-longitude pairs
        /// Implemented from: https://stackoverflow.com/questions/365826/calculate-distance-between-2-gps-coordinates
        /// </summary>
        private static double Distance (double lat1, double lon1, double lat2, double lon2) {
            double
            dlat = (lat2 - lat1) * DegreesToRadians,
            dlong = (lon2 - lon1) * DegreesToRadians,
            a = Math.Pow(Math.Sin(dlat / 2.0), 2) + 
                Math.Cos(lat1 * DegreesToRadians) *
                Math.Cos(lat2 * DegreesToRadians) *
                Math.Pow(Math.Sin(dlong / 2.0), 2),
            c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a)),
            distance = EarthRadius * c;
            return distance;
        }
        #endregion
    }
}