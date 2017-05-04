using UnityEngine;

public class CaptureScreen : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            // 截屏
            Cature();
        }
    }

    private void Cature()
    {
        string path = "F:/HelloWorld/Unity/Unity Tricks/Assets/NO26/Img/";
        string name = "test.png";

        // fileName截屏文件名称
        // superSize放大系数 默认为0即不放大
        Application.CaptureScreenshot(path + name, 0);
    }
}
