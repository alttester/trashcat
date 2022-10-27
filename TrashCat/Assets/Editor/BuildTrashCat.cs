﻿using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Altom.AltTester;
using Altom.AltTesterEditor;

public class BuildTrashCat
{

    [MenuItem("Build/WindowsWithAltTester")]
    static void WindowsBuildWithAltTester()
    {
        WindowsBuildFromCommandLine(true, 13000);
    }
    [MenuItem("Build/AndroidWithAltTester")]
    static void AndroidBuildWithAltTester()
    {
        string proxyHost = System.Environment.GetEnvironmentVariable("PROXY_HOST");

        AndroidBuildFromCommandLine(true, 13001, 13000);
    }

    [MenuItem("Build/macOSWithAltTester")]
    static void macOSBuildWithAltTester()
    {
        macOSBuildFromCommandLine(true, 13000);
    }

    [MenuItem("Build/macOSWithAltTesterIL2CPP")]
    static void macOSBuildWithAltTesterIL2CPP()
    {
        macOSBuildFromCommandLineIL2CPP(true, 13000);
    }

    [MenuItem("Build/WindowsWithAltTesterIL2CPP")]
    static void WindowsBuildWithAltTesterIL2CPP()
    {
        WindowsBuildFromCommandLineIL2CPP(true, 13000);
    }

    [MenuItem("Build/iOSWithAltTester")]
    static void iOSBuildWithAltTester()
    {
        string proxyHost = System.Environment.GetEnvironmentVariable("PROXY_HOST");

        iOSBuildFromCommandLine(true, proxyHost, 13000);
    }
    static void WindowsBuildFromCommandLine(bool withAltTester, int proxyPort = 13000)
    {
        SetPlayerSettings(false);

        Debug.Log("Starting Windows build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new string[]
        {
            "Assets/Scenes/Main.unity",
            "Assets/Scenes/Shop.unity",
            "Assets/Scenes/Start.unity"
        };
        if (withAltTester)
        {
            buildPlayerOptions.locationPathName = "Builds/TrashCatWindowsAltTester/TrashCat.exe";

        }
        else
        {
            buildPlayerOptions.locationPathName = "Builds/TrashCatWindows/TrashCat.exe";

        }
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.targetGroup = BuildTargetGroup.Standalone;
        if (withAltTester)
        {
            buildPlayerOptions.options = BuildOptions.Development;
        }
        BuildGame(buildPlayerOptions, withAltTester, proxyPort: proxyPort);

    }


    static void iOSBuildFromCommandLine(bool withAltTester, string proxyHost, int port = 13000)
    {
        PlayerSettings.companyName = "Altom";
        PlayerSettings.productName = "TrashCat";
        PlayerSettings.bundleVersion = "1.0";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, "fi.altom.trashcat");
        PlayerSettings.iOS.appleEnableAutomaticSigning = true;
        PlayerSettings.iOS.appleDeveloperTeamID = "59ESG8ELF5";
        PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_4_6);
        PlayerSettings.stripEngineCode = false;
        PlayerSettings.aotOptions = "ByteCode";

        Debug.Log("Starting iOS build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);


        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new string[]
        {
            "Assets/Scenes/Main.unity",
            "Assets/Scenes/Shop.unity",
            "Assets/Scenes/Start.unity"
        };
        if (withAltTester)
        {
            buildPlayerOptions.locationPathName = "Builds/TrashCatiOSAltTester/TrashCatiOS";
        }
        else
        {
            buildPlayerOptions.locationPathName = "Builds/TrashCatiOS/TrashCatiOS";

        }
        buildPlayerOptions.target = BuildTarget.iOS;
        buildPlayerOptions.targetGroup = BuildTargetGroup.iOS;
        if (withAltTester)
        {
            buildPlayerOptions.options = BuildOptions.Development;
        }
        BuildGame(buildPlayerOptions, withAltTester, proxyHost, port);

    }
    static void AndroidBuildFromCommandLine(bool withAltTester, string proxyHost, int proxyPort = 13000)
    {
        SetPlayerSettings(false);

        Debug.Log("Starting Android build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new string[]
        {
            "Assets/Scenes/Main.unity",
            "Assets/Scenes/Shop.unity",
            "Assets/Scenes/Start.unity"
        };
        if (withAltTester)
        {
            buildPlayerOptions.locationPathName = "Builds/TraschatAndroidAltTester/TrashCat.apk";
        }
        else
        {
            buildPlayerOptions.locationPathName = "Builds/TraschatAndroid/TrashCat.apk";

        }
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.targetGroup = BuildTargetGroup.Android;
        if (withAltTester)
        {
            buildPlayerOptions.options = BuildOptions.Development;
        }
        BuildGame(buildPlayerOptions, withAltTester, proxyHost, proxyPort);

    }

    private static void macOSBuildFromCommandLine(bool withAltTester, int proxyPort = 13000)
    {
        SetPlayerSettings(false);
        PlayerSettings.macRetinaSupport = true;

        Debug.Log("Starting Mac build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new string[]
        {
            "Assets/Scenes/Main.unity",
            "Assets/Scenes/Shop.unity",
            "Assets/Scenes/Start.unity"
        };
        if (withAltTester)
        {
            buildPlayerOptions.locationPathName = "Builds/TraschatmacOSAltTester/TrashCat.app";

        }
        else
        {
            buildPlayerOptions.locationPathName = "Builds/TraschatmacOS/TrashCat.app";

        }
        buildPlayerOptions.target = BuildTarget.StandaloneOSX;
        buildPlayerOptions.targetGroup = BuildTargetGroup.Standalone;
        if (withAltTester)
        {
            buildPlayerOptions.options = BuildOptions.Development;
        }

        BuildGame(buildPlayerOptions, withAltTester, proxyPort: proxyPort);

    }

    private static void macOSBuildFromCommandLineIL2CPP(bool withAltTester, int proxyPort = 13000)
    {
        SetPlayerSettings(true);
        PlayerSettings.macRetinaSupport = true;

        Debug.Log("Starting Mac build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new string[]
        {
            "Assets/Scenes/Main.unity",
            "Assets/Scenes/Shop.unity",
            "Assets/Scenes/Start.unity"
        };
        if (withAltTester)
        {
            buildPlayerOptions.locationPathName = "Builds/TraschatmacOSAltTesterIL2CPP/TrashCatTestIL2CPP.app";

        }
        else
        {
            buildPlayerOptions.locationPathName = "Builds/TraschatmacOSIL2CPP/TrashCatIL2CPP.app";

        }
        buildPlayerOptions.target = BuildTarget.StandaloneOSX;
        buildPlayerOptions.targetGroup = BuildTargetGroup.Standalone;
        if (withAltTester)
        {
            buildPlayerOptions.options = BuildOptions.Development;
        }

        BuildGame(buildPlayerOptions, withAltTester, proxyPort: proxyPort);

    }

    static void WindowsBuildFromCommandLineIL2CPP(bool withAltTester, int proxyPort = 13000)
    {
        SetPlayerSettings(true);

        Debug.Log("Starting Windows build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new string[]
        {
            "Assets/Scenes/Main.unity",
            "Assets/Scenes/Shop.unity",
            "Assets/Scenes/Start.unity"
        };
        if (withAltTester)
        {
            buildPlayerOptions.locationPathName = "Builds/TrashCatWindowsAltTesterIL2CPP/TrashCatIL2CPP.exe";

        }
        else
        {
            buildPlayerOptions.locationPathName = "Builds/TrashCatWindowsIL2CPP/TrashCatIL2CPP.exe";

        }
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;
        buildPlayerOptions.targetGroup = BuildTargetGroup.Standalone;
        if (withAltTester)
        {
            buildPlayerOptions.options = BuildOptions.Development;
        }
        BuildGame(buildPlayerOptions, withAltTester, proxyPort: proxyPort);

    }



    private static void SetPlayerSettings(bool customBuild)
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
        if (customBuild)
        {
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP);
        }

    }
    static void BuildGame(BuildPlayerOptions buildPlayerOptions, bool withAltTester, string proxyHost = null, int proxyPort = 13000)
    {
        try
        {
            if (withAltTester)
            {
                AddAltTester(buildPlayerOptions.targetGroup, buildPlayerOptions.scenes[0], proxyHost, proxyPort);
            }
            var results = BuildPipeline.BuildPlayer(buildPlayerOptions);

            if (withAltTester)
            {
                RemoveAltTester(buildPlayerOptions.targetGroup);
            }

            if (results.summary.totalErrors == 0 || results.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                Debug.Log("Build succeeded!");

            }
            else
            {
                Debug.LogError("Build failed! " + results.steps + "\n Result: " + results.summary.result + "\n Stripping info: " + results.strippingInfo);
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
    static void AddAltTester(BuildTargetGroup buildTargetGroup, string firstSceneName, string proxyHost = null, int proxyPort = 13000)
    {
        AltBuilder.PreviousScenePath = firstSceneName;
        var instrumentationSettings = new AltInstrumentationSettings();
        instrumentationSettings.ProxyPort = proxyPort;
        if (!string.IsNullOrEmpty(proxyHost)) instrumentationSettings.ProxyHost = proxyHost;
        AltBuilder.AddAltTesterInScriptingDefineSymbolsGroup(buildTargetGroup);
        AltBuilder.InsertAltInScene(firstSceneName, instrumentationSettings);
        Debug.Log("Instrumenting with proxyHost: " + proxyHost + ", proxyPort: " + proxyPort);

    }
    static void RemoveAltTester(BuildTargetGroup buildTargetGroup)
    {
        AltBuilder.RemoveAltTesterFromScriptingDefineSymbols(buildTargetGroup);
    }

}
