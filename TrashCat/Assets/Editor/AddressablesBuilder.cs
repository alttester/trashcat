using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class AddressablesBuilder
{
    public static void BuildAddressablesForCurrentPlatform()
    {
        BuildTarget target = EditorUserBuildSettings.activeBuildTarget;

        // In batchmode, make sure platform is switched correctly
        if (Application.isBatchMode)
        {
            Debug.Log($"[AddressablesBuilder] Active Build Target before Switch: {target}");

            if (!IsCorrectTarget(target))
            {
                target = DetectBuildTargetFromCommandLine();
                if (target != BuildTarget.NoTarget)
                {
                    Debug.Log($"[AddressablesBuilder] Switching platform to: {target}");
                    EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroupFor(target), target);
                }
            }
        }

        Debug.Log($"[AddressablesBuilder] Building Addressables for {target}");
        AddressableAssetSettings.BuildPlayerContent();
    }

    private static bool IsCorrectTarget(BuildTarget target)
    {
        // You can extend this check if needed
        return target == BuildTarget.StandaloneWindows64
            || target == BuildTarget.StandaloneWindows
            || target == BuildTarget.StandaloneOSX
            || target == BuildTarget.Android
            || target == BuildTarget.iOS
            || target == BuildTarget.WebGL;
    }

    private static BuildTarget DetectBuildTargetFromCommandLine()
    {
        string[] args = System.Environment.GetCommandLineArgs();

        foreach (string arg in args)
        {
            if (arg.Contains("WebGL"))
                return BuildTarget.WebGL;
            if (arg.Contains("StandaloneWindows"))
                return BuildTarget.StandaloneWindows64;
            if (arg.Contains("StandaloneOSX"))
                return BuildTarget.StandaloneOSX;
            if (arg.Contains("Android"))
                return BuildTarget.Android;
            if (arg.Contains("iOS"))
                return BuildTarget.iOS;
        }

        Debug.LogError("[AddressablesBuilder] Could not detect BuildTarget from command line args.");
        return BuildTarget.NoTarget;
    }

    private static BuildTargetGroup BuildTargetGroupFor(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return BuildTargetGroup.Standalone;
            case BuildTarget.StandaloneOSX:
                return BuildTargetGroup.Standalone;
            case BuildTarget.Android:
                return BuildTargetGroup.Android;
            case BuildTarget.iOS:
                return BuildTargetGroup.iOS;
            case BuildTarget.WebGL:
                return BuildTargetGroup.WebGL;
            default:
                return BuildTargetGroup.Unknown;
        }
    }
}
