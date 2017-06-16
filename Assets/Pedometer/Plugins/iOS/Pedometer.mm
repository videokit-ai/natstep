//
//  Pedometer.mm
//  Pedometer
//
//  Created by Yusuf on 06/15/17.
//  Copyright (c) 2017 Yusuf Olokoba
//

#import <CoreMotion/CoreMotion.h>

#define BRIDGE extern "C"
#define STEP2METERS 0.715

static CMPedometer* pedometer;

BRIDGE void PDInitialize () {
    // Create an instance
    pedometer = [CMPedometer new];
    // Start updates
    [pedometer startPedometerUpdatesFromDate:[NSDate date] withHandler:^(CMPedometerData* data, NSError* error) {
        // Extract data
        int steps = data.numberOfSteps.intValue;
        double distance = CMPedometer.isDistanceAvailable ? data.distance.doubleValue : steps * STEP2METERS;
        // Send to Unity
        UnitySendMessage("Pedometer", "OnEvent", [[NSString stringWithFormat:@"%i:%f", steps, distance] UTF8String]);
    }];
    // Log
    NSLog(@"%s", "Pedometer: Initialized iOS backend");
}

BRIDGE void PDRelease () {
    // Release and dereference
    if (pedometer) [pedometer stopPedometerUpdates];
    pedometer = nil;
    // Log
    NSLog(@"%s", "Pedometer: Released iOS backend");
}

BRIDGE bool PDIsSupported () {
    // Check if step counting is available
    return CMPedometer.isStepCountingAvailable;
}
