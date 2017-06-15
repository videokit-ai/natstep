/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU.Utilities {

    using UnityEditor;

    #if UNITY_IOS
	using UnityEditor.Callbacks;
	using UnityEditor.iOS.Xcode;
	using System.IO;
	#endif

    public static class PedometerEditor {

        private const string
		MotionUsageKey = @"NSMotionUsageDescription",
		MotionUsageDescription = @"Allow this app to use the pedometer."; // Change this as necessary

        [PostProcessBuild]
		static void SetPermissions (BuildTarget buildTarget, string path) {
			if (buildTarget != BuildTarget.iOS) return;
			string plistPath = path + "/Info.plist";
			PlistDocument plist = new PlistDocument();
			plist.ReadFromString(File.ReadAllText(plistPath));
			PlistElementDict rootDictionary = plist.root;
			rootDictionary.SetString(MotionUsageKey, MotionUsageDescription);
			File.WriteAllText(plistPath, plist.WriteToString());
		}
    }
}