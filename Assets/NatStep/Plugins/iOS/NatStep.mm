//
//  NatStep.mm
//  NatStep
//
//  Created by Yusuf on 06/15/17.
//  Copyright (c) 2017 Yusuf Olokoba
//

#import <CoreMotion/CoreMotion.h>

#define BRIDGE extern "C"
typedef void (*StepCallback) (int steps, double distance);


#pragma mark --NatStep--

@interface NatStep
- (id) initWithCallback:(StepCallback) callback;
- (void) release;
@property CMPedometer* pedometer;
@property (readonly) StepCallback callback;
@end

@implementation NatStep

- (id) initWithCallback:(LocationCallback) callback {
    // INCOMPLETE // Create and start pedometer updates
    return self;
}

- (void) release {
    
}
@end


#pragma mark --Bridge--

static NatStep* sharedInstance;

BRIDGE void NSInitialize (StepCallback callback) {
    // Create an instance and start listening
    sharedInstance = [[NatStep alloc] initWithCallback:callback];
}

BRIDGE void NSRelease () {
    if (sharedInstance) [sharedInstance release];
    sharedInstance = nil;
}
