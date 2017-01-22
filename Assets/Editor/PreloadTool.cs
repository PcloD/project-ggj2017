using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class PreloadTool
{
	static PreloadTool()
	{
		PlayerSettings.Android.keystorePass = "WeLoveGame";
		PlayerSettings.Android.keyaliasName = "dontdie";
        PlayerSettings.Android.keyaliasPass = "WeLoveGame";
	}
}