using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  // 导入DOTween命名空间

public class VRunning : MonoBehaviour
{
    // 垂直跑马灯
    private Text VText;
    [Header("背景高度")]
    public float bgHeight = 250.0f;
    [Header("持续时间")]
    public float duration = 5.0f;
    [Header("延迟时间")]
    public float delay = 1.0f;

    void Start()
    {
        VText = GetComponent<Text>();
        VPlay();
    }

    // 播放文字水平走马灯效果
    void VPlay()
    {
        // 获取文字的长度
        float height = VText.preferredHeight;
        // 让文字从在最下边开始移动
        VText.rectTransform.anchoredPosition = new Vector2(0.5f, 1);
        // 设置动画持续时间
        Tweener tweener = VText.rectTransform.DOLocalMoveY(bgHeight + height, duration);
        // 设置动画延迟时间
        tweener.SetDelay(delay);
        // 设置动画播放方式
        tweener.SetEase(Ease.Linear);
        // 每次播放结束后重新开始播放，一共播放5次
        tweener.SetLoops(5, LoopType.Yoyo);
        // 设置动画开始事件
        tweener.OnStart(delegate { Debug.Log("垂直走马灯事件开始"); });
        // 设置动画结束事件
        tweener.OnComplete(delegate { Debug.Log("垂直走马灯事件结束"); });
    }
}
