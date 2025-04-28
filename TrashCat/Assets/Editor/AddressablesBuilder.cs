using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public static class AddressablesBuilder
{
    public static void BuildAddressablesForCurrentPlatform()
    {
        BuildTarget target = EditorUserBuildSettings.activeBuildTarget;

        switch (target)
        {
            case BuildTarget.StandaloneWindows64:
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneOSX:
            case BuildTarget.Android:
            case BuildTarget.iOS:
            case BuildTarget.WebGL:
                Debug.Log($"Building Addressables for {target}");
                AddressableAssetSettings.BuildPlayerContent();
                break;

            default:
                Debug.LogError($"Unsupported platform for Addressables build: {target}");
                break;
        }
    }
}
