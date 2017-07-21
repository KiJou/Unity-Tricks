using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeText : MonoBehaviour
{
    [Header("持续时间")]
    public float duartion = 1.0f;

    void Start()
    {
        // endVuale duration
        Tweener tweener = GetComponent<Text>().DOFade(0, duartion);
        // 线性速度
        tweener.SetEase(Ease.Linear);
        // 无限循环
        tweener.SetLoops(-1, LoopType.Yoyo);
    }
}
