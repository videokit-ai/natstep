# Pedometer API
Pedometer is a native pedometer API for the Unity game engine. The API provides sensor information about step count and distance covered (in meters). It is a minimalist API with native backends on iOS and Android.

## Using the API
To use the API, simply create a `Pedometer` instance:
```csharp
// Create a pedometer
// The OnStep callback will be invoked each time a step is detected
var pedometer = new Pedometer(OnStep);

void OnStep (int steps, double distance) {
    // Display the values
    stepText.text = steps.ToString();
    // Display distance in feet
    distanceText.text = (distance * 3.28084).ToString("F2") + " ft";
}
```

When you are done detecting steps from a `Pedometer` instance, you must dispose it:
```csharp
// Dispose of this pedometer instance
pedometer.Dispose();
```

## Notes
- On iOS, you must add an `NSMotionUsageDescription` key to the `Info.plist` in Xcode before building. This determines the message used to request pedometer permissions from the user.

## Credits
- [Yusuf Olokoba](mailto:olokobayusuf@gmail.com)