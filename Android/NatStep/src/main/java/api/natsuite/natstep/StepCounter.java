/*
*   StepCounter.java
*   NatStep
*
*   Created by Yusuf on 01/14/2020.
*   Copyright (c) 2021 Yusuf Olokoba. All rights reserved.
*/

package api.natsuite.natstep;

import android.app.Application;
import android.content.Context;
import android.content.pm.PackageManager;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.util.Log;

/**
 * Step counter.
 */
public final class StepCounter implements SensorEventListener {

    //region --Client API--

    public interface Handler {
        /**
         * Method invoked when a new step is reported.
         * @param steps Aggregate number of steps detected by the step counter.
         * @param timestamp Timestamp of the step event in nanoseconds.
         */
        void onStep (int steps, long timestamp);
    }

    /**
     * Create a step counter.
     * @param handler Handler to receive step events.
     */
    public StepCounter (Handler handler) {
        this.handler = handler;
        try {
            Application application = (Application)Class
                .forName("android.app.ActivityThread")
                .getMethod("currentApplication")
                .invoke(null, (Object[]) null);
            this.manager = (SensorManager)application.getSystemService(Context.SENSOR_SERVICE);
            final Sensor sensor = manager.getDefaultSensor(Sensor.TYPE_STEP_COUNTER);
            manager.registerListener(this, sensor, SensorManager.SENSOR_DELAY_UI);
        } catch (Exception ex) {
            Log.e("NatSuite", "NatStep Error: Failed to acquire sensor manager", ex);
        }
    }

    /**
     * Release the step counter.
     */
    public void release () {
        if (manager != null)
            manager.unregisterListener(this);
    }

    /**
     * Does the device have a pedometer sensor.
     */
    public static boolean isAvailable () {
        try {
            Application application = (Application)Class
                    .forName("android.app.ActivityThread")
                    .getMethod("currentApplication")
                    .invoke(null, (Object[]) null);
            SensorManager manager = (SensorManager)application.getSystemService(Context.SENSOR_SERVICE);
            boolean supportsSensor = application.getPackageManager().hasSystemFeature(PackageManager.FEATURE_SENSOR_STEP_COUNTER);
            boolean hasSensor = manager.getDefaultSensor(Sensor.TYPE_STEP_COUNTER) != null;
            return supportsSensor && hasSensor;
        } catch (Exception ex) {
            Log.e("NatSuite", "NatStep Error: Failed to acquire sensor manager", ex);
            return false;
        }
    }
    //endregion

    //region --Operations--

    private final Handler handler;
    private SensorManager manager;

    @Override
    public void onAccuracyChanged (Sensor sensor, int accuracy) {}

    @Override
    public void onSensorChanged (SensorEvent event) {
        handler.onStep((int)event.values[0], event.timestamp);
    }
    //endregion
}
