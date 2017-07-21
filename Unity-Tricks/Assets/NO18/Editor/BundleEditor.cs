using UnityEditor;
using UnityEngine;

public static class BundleEditor
{
    private static string resPath = Application.dataPath + "/NO18/AssetBundles/";

    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAssetBundles()
    {
        BuildPipeline.BuildAssetBundles(resPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
