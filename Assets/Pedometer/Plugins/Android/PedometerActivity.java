package com.yusufolokoba.pedometer;

import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.os.Bundle;
import android.widget.Toast;
import android.util.Log;
import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

/**
 * Pedometer
 * Created by Yusuf on 06/14/17.
 */
public class PedometerActivity extends UnityPlayerActivity implements SensorEventListener {

    //region --Client API--

    public void initialize () {
        Log.d("Unity", "Pedometer: initialize called");
    }

    public void release () {
        
    }

    public boolean isSupported () {
        return false;
    }
    //endregion


    //region --Callbacks--

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {}

    @Override
    public void onSensorChanged (SensorEvent event) {
        // Extract data
        final double
        STEP2METERS = 0.715d,
        steps = event.values[0],
        distance = steps * STEP2METERS;
        // Send to Unity
        UnityPlayer.UnitySendMessage("Pedometer", "OnEvent", String.format("%i:%f", steps, distance));
    }
    //endregion
}
