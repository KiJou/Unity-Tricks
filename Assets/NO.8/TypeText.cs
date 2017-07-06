using UnityEngine;
using UnityEngine.UI;

public class TypeText : MonoBehaviour
{
    // 文字组件
    private Text contentText;
    // 需要显示的文字
    private string content;
    // 延迟时间 
    private float typingTime = 0.1f;
    // 当前打印的字数
    private int nowLength = 0;

    void Start()
    {
        contentText = GetComponent<Text>();
        content = contentText.text;
        contentText.text = "";
        // InvokeRepeating(methodName, time, repeatRate)
        // 程序开始time秒后，每经过repeatRate秒就自动调用methodName函数
        InvokeRepeating("Typing", 0.0f, typingTime);
    }

    void Typing()
    {
        ++nowLength;
        // Substring(startIndex, length)
        // 从startIndex开始，截取length个字符
        contentText.text = content.Substring(0, nowLength);
        if (nowLength >= content.Length)
        {
            CancelInvoke();
        }
    }
}
