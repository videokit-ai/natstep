#
#   Pedometer
#   Copyright (c) 2017 Yusuf Olokoba
#

SDKLIB := /Users/yusuf/Documents/ADT/SDK/platforms/android-23/android.jar # Replace with your path to the Android base class library
UNITYLIB := /Applications/Unity/PlaybackEngines/AndroidPlayer/Variations/mono/Release/Classes/classes.jar # Replace with your path to Unity's classes.jar
SRC := PedometerActivity

build:
	@echo "Building..."
	javac -source 1.6 -target 1.6 "$(SRC).java" -bootclasspath $(SDKLIB) -classpath $(UNITYLIB) -d .
	jar cf "$(SRC).jar" com
	@rm -rf com
	@echo "Completed"
	@echo "------------------------------------------------------ "