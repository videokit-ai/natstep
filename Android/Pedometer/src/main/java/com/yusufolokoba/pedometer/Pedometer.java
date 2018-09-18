package com.yusufolokoba.pedometer;

import android.content.Context;
import android.content.pm.PackageManager;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.util.Log;
import com.unity3d.player.UnityPlayer;

/**
 * Pedometer
 * Created by yusuf on 9/17/18.
 */
public class Pedometer implements SensorEventListener {

    private SensorManager manager;
    private final PedometerDelegate delegate;

    //region --Client API--

    public Pedometer (PedometerDelegate delegate) {
        this.delegate = delegate;
        this.manager = (SensorManager) UnityPlayer.currentActivity.getSystemService(Context.SENSOR_SERVICE);
    }

    public void initialize () {
        final Sensor sensor = manager.getDefaultSensor(Sensor.TYPE_STEP_COUNTER);
        if (sensor == null) {
            Log.e("Unity", "Pedometer Error: Failed to acquire step counter sensor");
            return;
        }
        manager.registerListener(this, sensor, SensorManager.SENSOR_DELAY_UI);
        Log.d("Unity", "Pedometer: Initialized Android backend");
    }

    public void release () {
        manager.unregisterListener(this);
        Log.d("Unity", "Pedometer: Released Android backend");
    }

    public boolean isSupported () {
        return UnityPlayer.currentActivity.getPackageManager().hasSystemFeature(PackageManager.FEATURE_SENSOR_STEP_COUNTER);
    }
    //endregion


    //region --Callbacks--

    @Override
    public void onAccuracyChanged (Sensor sensor, int accuracy) {}

    @Override
    public void onSensorChanged (SensorEvent event) {
        // Extract data
        final double STEP2METERS = 0.715d;
        final int steps = (int)event.values[0];
        final double distance = steps * STEP2METERS;
        // Send to Unity
        delegate.onStep(steps, distance);
    }
    //endregion
}
