#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
using UnityEngine;
#endif

public static class AddressablesBuilder
{
    public static void BuildAddressablesForCurrentPlatform()
    {
#if UNITY_EDITOR
        string envPlatform = System.Environment.GetEnvironmentVariable("UNITY_CLI_PLATFORM");
        BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
        BuildTargetGroup group = BuildPipeline.GetBuildTargetGroup(target);

        Debug.Log($"[AddressablesBuilder] Current platform from Unity Editor: {target}");
        Debug.Log($"[AddressablesBuilder] UNITY_CLI_PLATFORM env variable: {envPlatform}");

        if (!string.IsNullOrEmpty(envPlatform))
        {
            switch (envPlatform)
            {
                case "Windows":
                    target = BuildTarget.StandaloneWindows64;
                    group = BuildTargetGroup.Standalone;
                    break;
                case "macOS":
                    target = BuildTarget.StandaloneOSX;
                    group = BuildTargetGroup.Standalone;
                    break;
                case "iOS":
                    target = BuildTarget.iOS;
                    group = BuildTargetGroup.iOS;
                    break;
                case "Android":
                    target = BuildTarget.Android;
                    group = BuildTargetGroup.Android;
                    break;
                case "WebGL":
                    target = BuildTarget.WebGL;
                    group = BuildTargetGroup.WebGL;
                    break;
                default:
                    Debug.LogWarning($"[AddressablesBuilder] Unknown UNITY_CLI_PLATFORM: {envPlatform}. Will use current Editor platform.");
                    break;
            }

            if (EditorUserBuildSettings.activeBuildTarget != target)
            {
                Debug.Log($"[AddressablesBuilder] Switching build target to {target}");
                EditorUserBuildSettings.SwitchActiveBuildTarget(group, target);
            }
        }

        Debug.Log($"[AddressablesBuilder] Building Addressables for platform: {target}");
        AddressableAssetSettings.BuildPlayerContent();
#endif
    }
}
