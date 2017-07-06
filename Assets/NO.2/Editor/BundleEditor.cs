using UnityEditor;
using UnityEngine;

public static class BundleEditor
{
    private static string resPath = Application.dataPath + "/Main/AssetBundles/";

    [MenuItem("Assets/Build")]
    static void BuildAssetBundles()
    {
        BuildPipeline.BuildAssetBundles(resPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
