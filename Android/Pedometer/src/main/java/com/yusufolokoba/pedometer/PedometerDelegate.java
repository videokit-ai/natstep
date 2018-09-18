package com.yusufolokoba.pedometer;

/**
 * Pedometer
 * Created by yusuf on 9/17/18.
 */
public interface PedometerDelegate {
    void onStep (int steps, double distance);
}
