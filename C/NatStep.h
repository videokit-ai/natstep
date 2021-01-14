//
//  NatStep.h
//  NatStep
//
//  Created by Yusuf Olokoba on 12/01/21.
//  Copyright © 2021 Yusuf Olokoba. All rights reserved.
//

#pragma once

#include "stdint.h"
#include "stdbool.h"

// Platform defines
#ifdef __cplusplus
    #define BRIDGE extern "C"
#else
    #define BRIDGE
#endif

/*!
 @abstract Callback invoked with a reported step.
 
 @param context
 User context provided to payload.
 
 @param steps
 Number of steps aggregated by the step counter.

 @param timestamp
 Timestamp of step event in nanoseconds.
 */
typedef void (*NSStepHandler) (void* context, int32_t steps, int64_t timestamp);

/*!
 @function NSStepCounterAvailable
 
 @abstract Is there an available step counter sensor.
 
 @discussion Is there an available step counter sensor.
 
 @returns Whether there is an available step counter sensor.
 */
BRIDGE bool NSStepCounterAvailable (void);

/*!
 @function NSCreateStepCounter
 
 @abstract Create a step counter.
 
 @discussion Create a step counter.

 @param handler
 Handler to receive steps as they are reported.

 @param context
 User-provided context provided to the handler
 
 @returns Opaque pointer to created step counter.
 */
BRIDGE void* NSCreateStepCounter (NSStepHandler handler, void* context);

/*!
 @function NSCreateStepCounter
 
 @abstract Create a step counter.
 
 @discussion Create a step counter.

 @param stepCounter
 Opaque pointer to a step counter.
 */
BRIDGE void NSDisposeStepCounter (void* stepCounter);