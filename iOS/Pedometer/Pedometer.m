//
//  Pedometer.m
//  Pedometer
//
//  Created by Yusuf Olokoba on 9/17/18.
//  Copyright © 2018 Yusuf Olokoba. All rights reserved.
//

#import <CoreMotion/CoreMotion.h>

typedef void (*StepCallback) (int steps, double distance);

static CMPedometer* pedometer;

void PDInitialize (StepCallback callback) {
    pedometer = [CMPedometer new];
    [pedometer startPedometerUpdatesFromDate:NSDate.date withHandler:^(CMPedometerData* data, NSError* error) {
        const double StepsToMeters = 0.715;
        int steps = data.numberOfSteps.intValue;
        double distance = CMPedometer.isDistanceAvailable ? data.distance.doubleValue : steps * StepsToMeters;
        callback(steps, distance);
    }];
    NSLog(@"Pedometer: Initialized iOS backend");
}

void PDRelease () {
    if (pedometer)
        [pedometer stopPedometerUpdates];
    pedometer = nil;
    NSLog(@"Pedometer: Released iOS backend");
}

bool PDIsSupported () {
    return CMPedometer.isStepCountingAvailable;
}
