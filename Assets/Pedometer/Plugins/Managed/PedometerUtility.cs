/* 
*   Pedometer
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace PedometerU {

    using UnityEngine;
    using System;
    using System.Collections.Generic;

    [AddComponentMenu("")]
    public sealed class PedometerUtility : MonoBehaviour {

        private static Queue<Action> dispatchQueue = new Queue<Action>();
        private static readonly object dispatchFence = new object();

        public static void Initialize () {
            new GameObject("Pedometer Event Utility").AddComponent<PedometerUtility>();
        }

        public static void Dispatch (Action action) {
            lock (dispatchFence) if (action != null) dispatchQueue.Enqueue(action);
        }
        
        void Awake () {
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(this);
        }

        void Update () {
            lock (dispatchFence) while (dispatchQueue.Count > 0) dispatchQueue.Dequeue()();
        }
    }
}