package com.yusufolokoba.pedometer;

import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.content.pm.PackageManager;
import android.util.Log;
import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

/**
 * Pedometer
 * Created by Yusuf on 06/14/17.
 */
public class PedometerActivity extends UnityPlayerActivity implements SensorEventListener {

    private Sensor sensor;
    private SensorManager manager;

    //region --Client API--

    public void initialize () {
        // Get sensor manager
        manager = manager == null ? (SensorManager)getSystemService(Context.SENSOR_SERVICE) : manager;
        // Get sensor
        if ((sensor = manager.getDefaultSensor(Sensor.TYPE_STEP_COUNTER)) == null) {
            Log.e("Unity", "Pedometer Error: Failed to acquire step counter sensor");
            return;
        }
        // Start listening
        manager.registerListener(this, sensor, SensorManager.SENSOR_DELAY_UI);
        // Log
        Log.d("Unity", "Pedometer: Initialized Android backend");
    }

    public void release () {
        // Stop listening
        manager.unregisterListener(this);
        // Dereference
        sensor = null;
        // Log
        Log.d("Unity", "Pedometer: Released Android backend");
    }

    public boolean isSupported () {
        return getPackageManager().hasSystemFeature(PackageManager.FEATURE_SENSOR_STEP_COUNTER);
    }
    //endregion


    //region --Callbacks--

    @Override
    public void onAccuracyChanged (Sensor sensor, int accuracy) {}

    @Override
    public void onSensorChanged (SensorEvent event) {
        // Extract data
        final double
        STEP2METERS = 0.715d,
        steps = event.values[0],
        distance = steps * STEP2METERS;
        // Send to Unity
        UnityPlayer.UnitySendMessage("Pedometer", "OnEvent", String.format("%d:%f", (int)steps, distance));
    }
    //endregion
}
