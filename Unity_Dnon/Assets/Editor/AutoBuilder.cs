using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class AutoBuilder : MonoBehaviour
{
	[MenuItem("Build/Build WebGL")]
	static void PerformBuild()
	{
		var sceneSettings = EditorBuildSettings.scenes;
		string[] scenePaths = new string[sceneSettings.Length];

		for (int i = 0; i < scenePaths.Length; ++i)
		{
			scenePaths[i] = sceneSettings[i].path;
		}
		BuildPipeline.BuildPlayer(scenePaths, "build/webgl", BuildTarget.WebGL, BuildOptions.Development);
	}
}
