using System.Collections;
using UnityEngine;

public class LoadAssetBundle : MonoBehaviour
{
    // 本地资源地址
    private string localPath;
    // 网络资源地址
    private string netPath;
    // 需要加载的资源名字
    private string prefabName;
    // 保存物体
    private GameObject go;

    void Start()
    {
        localPath = Application.dataPath + "/Main/AssetBundles/";
        netPath = "http://www.littleredhat1997.com/game/AssetBundles/";
        prefabName = "player";
    }

    void OnGUI()
    {
        // 从本地加载
        if (GUILayout.Button("Load From Local"))
        {
            Debug.Log(localPath + prefabName);
            StartCoroutine(Load(localPath + prefabName));
        }
        // 从网络加载
        if (GUILayout.Button("Load From Net"))
        {
            Debug.Log(netPath + prefabName);
            StartCoroutine(Load(netPath + prefabName));
        }
        // 销毁物体
        if (GUILayout.Button("Destroy Game Object"))
        {
            DestroyTheObject();
        }
    }

    // 从本地 / 网络加载
    IEnumerator Load(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError("网络错误：" + www.error);
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
