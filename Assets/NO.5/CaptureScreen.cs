using UnityEngine;

public class CaptureScreen : MonoBehaviour
{

    void OnGUI()
    {
        // 截屏
        if (GUILayout.Button("截屏"))
        {
            Debug.Log("截屏");
            Cature();
        }
    }

    void Cature()
    {
        string path = Application.dataPath + "/Main/Screen/";
        string guid = System.Guid.NewGuid().ToString() + ".png";
        // fileName截屏文件名称
        // superSize放大系数 默认为0即不放大
        ScreenCapture.CaptureScreenshot(path + guid, 0);
    }
}
