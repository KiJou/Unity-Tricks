using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TypeText : MonoBehaviour
{
    [Header("持续时间")]
    public float duartion = 5.0f;

    void Start()
    {
        Text textUI = GetComponent<Text>();
        string content = textUI.text;
        textUI.text = "";

        // endVuale duration
        Tweener tweener = textUI.DOText(content, duartion);
        // 线性速度
        tweener.SetEase(Ease.Linear);
    }
}
