﻿using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BuildTrashCat
{



    [MenuItem("Build/WindowsInspectorWithAltunity")]
    static void WindowsBuildInspectorWithAltUnity()
    {
        WindowsBuildFromCommandLine(true, 13000);
    }
    static void WindowsBuildFromCommandLine(bool withAltunity, int port = 13000)
    {
        SetPlayerSettings();

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
            buildPlayerOptions.locationPathName = "TrashCatWindowsTest/TrashCat.exe";

        }
        else
        {
            buildPlayerOptions.locationPathName = "TrashCatWindows/TrashCat.exe";

        }
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;
        buildPlayerOptions.targetGroup = BuildTargetGroup.Standalone;
        if (withAltunity)
        {
            buildPlayerOptions.options = BuildOptions.Development;
        }
        BuildGame(buildPlayerOptions, withAltunity, port);

    }
    
    [MenuItem("Build/macOSWithAltUnity")]
    static void MacOSBuildInspectorWithAltUnity()
    {
        MacOSBuildFromCommandLine(true, 13000);
    }
    private static void MacOSBuildFromCommandLine(bool withAltUnity, int port = 13000)
    {
        SetPlayerSettings();
        PlayerSettings.macRetinaSupport = true;

        Debug.Log("Starting Mac build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new string[]
        {
            "Assets/Scenes/Main.unity",
            "Assets/Scenes/Shop.unity",
            "Assets/Scenes/Start.unity"
        };
        if (withAltUnity)
        {
            buildPlayerOptions.locationPathName = "TrashCatTest.app";

        }
        else
        {
            buildPlayerOptions.locationPathName = "TrashCat.app";

        }
        buildPlayerOptions.target = BuildTarget.StandaloneOSX;
        buildPlayerOptions.targetGroup = BuildTargetGroup.Standalone;
        if (withAltUnity)
        {
            buildPlayerOptions.options = BuildOptions.Development;
        }

        BuildGame(buildPlayerOptions, withAltUnity, port);

    }



    private static void SetPlayerSettings()
    {
            PlayerSettings.companyName = "Altom";
            PlayerSettings.productName = "TrashCat";
            PlayerSettings.bundleVersion = "1.0";
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
