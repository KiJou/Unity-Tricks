using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
    private WebCamTexture tex;

    void Start()
    {
        // 开启摄像头
        StartCoroutine(startCamera());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            // 拍照
            StartCoroutine(getPhoto());
        }
    }

    IEnumerator startCamera()
    {
        // 请求用户授权
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        // 用户是否具有授权
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            string deviceName = devices[0].name;
            // deviceName requestedWidth requestedHeight requestedFPS
            // 创建网络相机贴图
            tex = new WebCamTexture(deviceName, Screen.width, Screen.height, 12);
            // 循环模式Repeat重复或Clamp强制拉伸
            tex.wrapMode = TextureWrapMode.Repeat;
            // 将摄像头实时内容渲染到RawImage上
            GetComponent<RawImage>().texture = tex;
            tex.Play();
        }
    }

    IEnumerator getPhoto()
    {
        string path = Application.dataPath + "/NO27/Screen/";
        string guid = System.Guid.NewGuid().ToString() + ".png";

        yield return new WaitForEndOfFrame();
        // width height format mipmap
        // 创建二维纹理
        Texture2D t = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        // source destX destY recalculateMipmaps
        // 读取屏幕像素信息并存储为纹理数据
        t.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        t.Apply();
        // 纹理编码为PNG格式
        byte[] bs = t.EncodeToPNG();
        File.WriteAllBytes(path + guid, bs);
        tex.Pause();
    }
}
