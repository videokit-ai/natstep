//
//  Bridge.cpp
//  NatStep
//
//  Created by Yusuf Olokoba on 14/01/2021.
//  Copyright © 2021 Yusuf Olokoba. All rights reserved.
//

#include <jni.h>
#include "NatStep.h"

static JNIEnv* GetEnv ();
static jobject CreateCallback (JNIEnv* env, NSStepHandler completionHandler, void* context);

#pragma region --NatStep--

bool NSStepCounterAvailable () {

}

void* NSCreateStepCounter (NSStepHandler handler, void* context) {

}

void NSDisposeStepCounter (void* stepCounter) {

}
#pragma endregion


#pragma region --JNI--

static JavaVM* jvm;

BRIDGE JNIEXPORT jint JNICALL JNI_OnLoad (JavaVM* vm, void* reserved) {
    jvm = vm;
    return JNI_VERSION_1_6;
}

JNIEnv* GetEnv () {
    JNIEnv* env = nullptr;
    int status = jvm->GetEnv(reinterpret_cast<void**>(&env), JNI_VERSION_1_6);
    if (status == JNI_EDETACHED)
        jvm->AttachCurrentThread(&env, nullptr);
    return env;
}

jobject CreateCallback (JNIEnv* env, NSStepHandler completionHandler, void* context) {
    jclass clazz = env->FindClass("api/natsuite/natstep/NativeCallback");
    jmethodID constructor = env->GetMethodID(clazz, "<init>", "(JJ)V");
    jobject object = env->NewObject(clazz, constructor, (jlong)(void*)completionHandler, (jlong)context);
    env->DeleteLocalRef(clazz);
    return object;
}
#pragma endregion