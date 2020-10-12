﻿using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BuildTrashCat
{


    [MenuItem("Build/Windows")]
    static void WindowsBuildInspector()
    {
        WindowsBuildFromCommandLine(false);
    }

    [MenuItem("Build/WindowsInspectorWithAltunity")]
    static void WindowsBuildInspectorWithAltUnity()
    {
        WindowsBuildFromCommandLine(true, 13001);
    }
    static void WindowsBuildFromCommandLine(bool withAltunity, int port = 13000)
    {
        // SetPlayerSettingsForInspector();

        Debug.Log("Starting Windows build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new string[]
        {
            "Assets/Scenes/Main.unity",
            "Assets/Scenes/Shop.unity",
            "Assets/Scenes/Start.unity"
        };
        if (withAltunity)
        {
            buildPlayerOptions.locationPathName = "AltUnityInspectorWindowsTest/AltUnityInspector.exe";

        }
        else
        {
            buildPlayerOptions.locationPathName = "AltUnityInspectorWindows/AltUnityInspector.exe";

        }
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;
        buildPlayerOptions.targetGroup = BuildTargetGroup.Standalone;
        if (withAltunity)
        {
            buildPlayerOptions.options = BuildOptions.Development;
        }
        BuildGame(buildPlayerOptions, withAltunity, port);

    }

    private static void SetPlayerSettingsForInspector()
    {
        using (StreamReader reader = new StreamReader("Assets/version.txt"))
        {
            string versionNumber = reader.ReadLine();
            PlayerSettings.companyName = "Altom";
            PlayerSettings.productName = "AltUnityInspectorAlpha";
            PlayerSettings.bundleVersion = versionNumber;
            PlayerSettings.resizableWindow = true;
            PlayerSettings.defaultScreenHeight = 900;
            PlayerSettings.defaultScreenWidth = 1200;
            PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
            Texture2D icon = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/icon.png");
            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, ApiCompatibilityLevel.NET_4_6);
            PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, new Texture2D[] { icon });
            PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Standalone, new Texture2D[] { icon, icon, icon, icon, icon, icon, icon, icon });
            PlayerSettings.runInBackground = true;
        }
    }

    

    [MenuItem("Build/WindowsSampleSceneWithAltunity")]
    static void BuildWindowsSampleScenesWithAltunity()
    {
        SetPlayerSettingForSampleScenes();

        Debug.Log("Starting Windows build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions = SetScenesForSampleScene(buildPlayerOptions);

        buildPlayerOptions.locationPathName = "SampleScene/TrashCat.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;
        buildPlayerOptions.targetGroup = BuildTargetGroup.Standalone;
        buildPlayerOptions.options = BuildOptions.Development;

        BuildGame(buildPlayerOptions, true);

    }
    
    private static BuildPlayerOptions SetScenesForSampleScene(BuildPlayerOptions buildPlayerOptions)
    {
        buildPlayerOptions.scenes = new string[]
         {
            "Assets/Plugins/AltUnityTester/Assets/AltUnityTester/Examples/Scenes/Scene 1 AltUnityDriverTestScene.unity",
            "Assets/Plugins/AltUnityTester/Assets/AltUnityTester/Examples/Scenes/Scene 2 Draggable Panel.unity",
            "Assets/Plugins/AltUnityTester/Assets/AltUnityTester/Examples/Scenes/Scene 3 Drag And Drop.unity"

        };
        return buildPlayerOptions;
    }

    private static void SetPlayerSettingForSampleScenes()
    {
        string versionNumber = DateTime.Now.ToString("yyMMddHHss");
        PlayerSettings.companyName = "Altom";
        PlayerSettings.productName = "sampleGame";
        PlayerSettings.bundleVersion = versionNumber;
        PlayerSettings.Android.bundleVersionCode = int.Parse(versionNumber);
        PlayerSettings.resizableWindow = true;
        PlayerSettings.defaultScreenHeight = 900;
        PlayerSettings.defaultScreenWidth = 1200;
        PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
        PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, ApiCompatibilityLevel.NET_4_6);
        PlayerSettings.runInBackground = true;
    }

    static void BuildGame(BuildPlayerOptions buildPlayerOptions, bool withAltUnity, int port = 13000)
    {
        try
        {
            if (withAltUnity)
            {
                AddAltUnity(buildPlayerOptions.targetGroup, buildPlayerOptions.scenes[0], port);
            }
            var results = BuildPipeline.BuildPlayer(buildPlayerOptions);

            if (withAltUnity)
            {
                RemoveAltUnity(buildPlayerOptions.targetGroup);
            }

            if (results.summary.totalErrors == 0)
            {
                Debug.Log("No Build Errors");

            }
            else
            {
                Debug.LogError("Build Error! " + results.steps + "\n Result: " + results.summary.result + "\n Stripping info: " + results.strippingInfo);
                // EditorApplication.Exit(1);
            }

            Debug.Log("Finished. " + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
            // EditorApplication.Exit(0);
        }
        catch (Exception exception)
        {

            Debug.LogException(exception);
            // EditorApplication.Exit(1);
        }
    }
    static void AddAltUnity(BuildTargetGroup buildTargetGroup, string firstSceneName, int port = 13000)
    {
        AltUnityBuilder.PreviousScenePath = firstSceneName;
        AltUnityBuilder.AddAltUnityTesterInScritpingDefineSymbolsGroup(buildTargetGroup);
        AltUnityBuilder.InsertAltUnityInScene(firstSceneName, port);

    }
    static void RemoveAltUnity(BuildTargetGroup buildTargetGroup)
    {
        AltUnityBuilder.RemoveAltUnityTesterFromScriptingDefineSymbols(buildTargetGroup);
    }

    
    
}
