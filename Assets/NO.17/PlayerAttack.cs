using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerAttack : MonoBehaviour
{
    [Header("攻击方向")]
    public Vector2 playDir;
    [Header("血条")]
    public Text hpText;
    [Header("飘血")]
    public Text damageText;
    [Header("冲击力量")]
    public int punch = 100;
    [Header("血量上限")]
    public int allHp = 100;
    [Header("当前血量")]
    public int nowHp = 100;

    private bool isGameOver = false;
    public bool IsGameOver { get { return isGameOver; } }

    void Start()
    {
        // 初始化
        nowHp = allHp;
        damageText.DOFade(0f, 0f);
        hpText.text = nowHp + " / " + allHp;
    }

    // 玩家攻击
    public void Attack()
    {
        // Back and Forth ==> punch duration
        transform.GetComponent<RectTransform>().DOPunchAnchorPos(playDir * punch, 0.2f);
    }

    // 玩家受伤
    public void Damage(int lossHp = 1)
    {
        // 扣血逻辑
        nowHp -= lossHp;
        if (nowHp <= 0)
            isGameOver = true;
        // 显示当前血量
        hpText.text = nowHp + " / " + allHp;
        // 显示扣血飘血
        damageText.text = "- " + lossHp;
        // Fade In ==> endValue duration
        damageText.DOFade(1.0f, 0f);
        // Fade Out ==> endValue duration
        damageText.DOFade(0f, 1.5f);
    }
}
