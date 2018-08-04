/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

using UnityEditor.Build.Reporting;

namespace PedometerU.Utilities {

    using UnityEditor;
	using UnityEditor.Build;
	using System;
	using System.IO;

    #if UNITY_IOS
	using UnityEditor.Callbacks;
	using UnityEditor.iOS.Xcode;
	#endif

    public class PedometerEditor : IPreprocessBuildWithReport, IPostprocessBuildWithReport, IOrderedCallback {

		private string AndroidPlugins => Path.Combine(Environment.CurrentDirectory, "Assets/Plugins/Android");
		int IOrderedCallback.callbackOrder { get; } = 0;

        private const string
		MotionUsageKey = @"NSMotionUsageDescription",
		MotionUsageDescription = @"Allow this app to use the pedometer."; // Change this as necessary

		void IPreprocessBuildWithReport.OnPreprocessBuild (BuildReport report) {
			#if UNITY_ANDROID
			// Create the Android plugins directory
			Directory.CreateDirectory(AndroidPlugins);
			// Copy the manifest into it // This is the only place where Unity picks up manifests, so we have to copy into it
			File.Copy(
				Path.Combine(
					Environment.CurrentDirectory,
					"Assets/Pedometer/Plugins/Android/AndroidManifest.xml"
				),
				Path.Combine(
					AndroidPlugins,
					"AndroidManifest.xml"
				)
			);				
			#endif
		}

		void IPostprocessBuildWithReport.OnPostprocessBuild (BuildReport report) {
			#if UNITY_ANDROID
			// Delete the Android manifest from Plugins/Android
			File.Delete(Path.Combine(AndroidPlugins, "AndroidManifest.xml"));
			#elif UNITY_IOS
			// Get the property list
			string plistPath = report.summary.outputPath + "/Info.plist";
			PlistDocument plist = new PlistDocument();
			// Read it
			plist.ReadFromString(File.ReadAllText(plistPath));
			PlistElementDict rootDictionary = plist.root;
			// Add the motion usage description
			rootDictionary.SetString(MotionUsageKey, MotionUsageDescription);
			File.WriteAllText(plistPath, plist.WriteToString());
			#endif
		}
    }
}
