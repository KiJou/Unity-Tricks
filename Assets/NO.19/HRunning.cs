using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  // 导入DOTween命名空间

public class HRunning : MonoBehaviour
{
    // 水平跑马灯
    private Text HText;
    [Header("背景宽度")]
    public float bgWidth = 400.0f;
    [Header("持续时间")]
    public float duration = 5.0f;
    [Header("延迟时间")]
    public float delay = 1.0f;

    void Start()
    {
        HText = GetComponent<Text>();
        HPlay();
    }

    // 播放文字水平走马灯效果
    private void HPlay()
    {
        // 获取文字的长度
        float width = HText.preferredWidth;
        // 让文字从在最右边开始移动
        HText.rectTransform.anchoredPosition = new Vector2(0, 0.5f);
        // 设置动画持续时间
        Tweener tweener = HText.rectTransform.DOLocalMoveX(-(bgWidth + width), duration);
        // 设置动画延迟时间
        tweener.SetDelay(delay);
        // 设置动画播放方式 
        tweener.SetEase(Ease.Linear);
        // 每次播放结束后重新开始播放，一共播放5次
        tweener.SetLoops(5, LoopType.Restart);
        // 设置动画开始事件
        tweener.OnStart(delegate { Debug.Log("水平走马灯事件开始"); });
        // 设置动画结束事件
        tweener.OnComplete(delegate { Debug.Log("水平走马灯事件结束"); });
    }
}
