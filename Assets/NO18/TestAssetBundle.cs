using System.Collections;
using UnityEngine;

public class TestAssetBundle : MonoBehaviour
{
    // 本地资源地址
    private const string localPath = "file://F:/HelloWorld/Unity/Unity Tricks/Assets/NO18/AssetBundles/";
    // 网络资源地址
    private const string netPath = "http://www.littleredhat1997.com/code/AssetBundles/";
    // 需要加载的资源名字
    private const string prefabName = "swordman";

    // 保存物体
    private GameObject go;

    void OnGUI()
    {
        // 从本地加载
        if (GUILayout.Button("Load From Local"))
        {
            Debug.Log(localPath + prefabName);
            StartCoroutine(LoadAssetBundles(localPath + prefabName));
        }
        // 从网络加载
        if (GUILayout.Button("Load From Net"))
        {
            Debug.Log(netPath + prefabName);
            StartCoroutine(LoadAssetBundles(netPath + prefabName));
        }
        // 销毁物体
        if (GUILayout.Button("Destroy Game Object"))
        {
            DestroyTheObject();
        }
    }

    // 从本地 / 网络加载
    IEnumerator LoadAssetBundles(string url)
    {
        WWW www = new WWW(url);

        yield return www;
        if (www.error != null)
        {
            Debug.LogError("网络错误");
        }
        else
        {
            AssetBundle bundle = www.assetBundle;
            // 加载资源
            Object obj = bundle.LoadAsset(prefabName);
            go = Instantiate(obj) as GameObject;
            // 释放加载的资源
            bundle.Unload(false);
        }
    }

    // 销毁物体
    void DestroyTheObject()
    {
        if (go != null)
        {
            Destroy(go);
        }
    }
}
