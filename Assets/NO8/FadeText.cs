using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeText : MonoBehaviour
{
    private float delay = 1.0f;

    void Start()
    {
        // delay时间内隐藏
        Tweener tweener = GetComponent<Text>().DOFade(0, delay);
        // 线性速度渐变
        tweener.SetEase(Ease.Linear);
        // -1表示无限循环
        tweener.SetLoops(-1, LoopType.Yoyo);
    }
}
