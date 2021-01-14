# NatStep API
NatStep is a cross-platform step counting API for Unity Engine. The API provides sensor information about step count and distance covered (in meters).

## Setup Instructions
NatStep should be installed using the Unity Package Manager. In your package.json file, add the following dependency:
```json
{
  "dependencies": {
    "com.natsuite.natstep": "git+https://github.com/natsuite/NatStep?path=/Packages/com.natsuite.natstep"
  }
}
```

## Counting Steps
To use the API, simply create a step counter:
```csharp
// Create a pedometer
// The OnStep callback will be invoked each time a step is detected
var stepCounter = new StepCounter(OnStep);
```

As the user walks around, the pedometer sensor will report steps to the delegate provided:
```csharp
void OnStep (int steps, long timestamp) {
    // Display the number of steps on a UI element
    stepText.text = $"{steps}";
}
```

When you are done detecting steps, dispose the step counter:
```csharp
// Dispose the step counter
stepCounter.Dispose();
```

## iOS Instructions
After building an Xcode project from Unity, add the following keys to the `Info.plist` file with a good description:
- `NSMotionUsageDescription`

## Requirements
- Unity 2019.2+
- Android API level 24+
- iOS 11+