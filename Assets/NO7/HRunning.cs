using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  // 导入 DOTween 命名空间

public class HRunning : MonoBehaviour
{
    // 水平跑马灯
    public Text HText;
    public float bgWidth = 200.0f;  // 400 / 2 即 宽度除以2
    public float duration = 2.0f;
    public float delay = 1.0f;

    void Start()
    {
        HPlay();
    }

    // 播放文字水平走马灯效果
    private void HPlay()
    {
        float width = HText.preferredWidth;  // 获取文字的长度
        HText.rectTransform.anchoredPosition = new Vector2(0, 0);  // 让文字从在最右边开始移动  

        Tweener tweener = HText.rectTransform.DOLocalMoveX(-(bgWidth + width), duration);  // 设置动画持续时间
        tweener.SetDelay(delay);  // 设置动画延迟时间
        tweener.SetEase(Ease.Linear);  // 设置动画播放方式
        tweener.OnStart(delegate { Debug.Log("水平走马灯事件开始"); });  // 设置动画开始事件
        tweener.OnComplete(delegate { Debug.Log("水平走马灯事件结束"); });  // 设置动画结束事件
    }
}
