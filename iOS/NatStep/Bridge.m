//
//  Bridge.m
//  NatStep
//
//  Created by Yusuf Olokoba on 9/17/18.
//  Copyright © 2021 Yusuf Olokoba. All rights reserved.
//

@import CoreMotion;
#import "NatStep.h"

bool NSStepCounterAvailable () {
    return CMPedometer.isStepCountingAvailable;
}

void* NSCreateStepCounter (NSStepHandler handler, void* context) {
    CMPedometer* pedometer = CMPedometer.new;
    [pedometer startPedometerUpdatesFromDate:NSDate.date withHandler:^(CMPedometerData* data, NSError* error) {
        int32_t steps = data.numberOfSteps.intValue;
        int64_t timestamp = (int64_t)(data.endDate.timeIntervalSince1970 * 1e+9);
        handler(context, steps, timestamp);
    }];
    return (__bridge_retained void*)pedometer;
}

void NSDisposeStepCounter (void* stepCounter) {
    CMPedometer* pedometer = (__bridge_transfer CMPedometer*)stepCounter;
    pedometer = nil;
}
