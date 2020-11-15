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
<<<<<<< HEAD
		string[] scenePaths = { "Assets/Scenes/GameScene.unity" }; 
=======
		string[] scenePaths = { "Assets/Scenes/#2_Game.unity" }; 
>>>>>>> develop
			BuildPipeline.BuildPlayer(scenePaths, "build/webgl", BuildTarget.WebGL, BuildOptions.Development);
	}
}
