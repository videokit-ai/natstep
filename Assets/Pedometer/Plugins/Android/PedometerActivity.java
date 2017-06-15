package com.yusufolokoba.pedometer;

import com.unity3d.player.UnityPlayerActivity;
import android.hardware.SensorEventListener;

/**
 * Pedometer
 * Created by Yusuf on 06/14/17.
 */
public class PedometerActivity extends UnityPlayerActivity implements SensorEventListener {

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {}

    @Override
    public void onSensorChanged (SensorEvent event) {
        // Get the steps
        final float steps = event.values[0];
        // Do stuff...
    }
}
