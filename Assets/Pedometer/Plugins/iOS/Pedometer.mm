//
//  Pedometer.mm
//  Pedometer
//
//  Created by Yusuf on 06/15/17.
//  Copyright (c) 2017 Yusuf Olokoba
//

#import <CoreMotion/CoreMotion.h>

#define BRIDGE extern "C"
typedef void (*StepCallback) (int steps, double distance);


#pragma mark --Pedometer--

@interface Pedometer
- (id) initWithCallback:(StepCallback) callback;
- (void) release;
@property CMPedometer* pedometer;
@property (readonly) StepCallback callback;
@end

@implementation Pedometer

- (id) initWithCallback:(LocationCallback) callback {
    // INCOMPLETE // Create and start pedometer updates
    return self;
}

- (void) release {
    
}
@end


#pragma mark --Bridge--

static Pedometer* sharedInstance;

BRIDGE void Initialize (StepCallback callback) {
    // Create an instance and start listening
    sharedInstance = [[Pedometer alloc] initWithCallback:callback];
}

BRIDGE void Release () {
    if (sharedInstance) [sharedInstance release];
    sharedInstance = nil;
}
