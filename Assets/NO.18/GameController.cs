using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("玩家一攻击按钮")]
    public Button playerOneBtn;
    [Header("玩家二攻击按钮")]
    public Button playerTwoBtn;
    [Header("玩家一脚本")]
    public PlayerAttack playerOne;
    [Header("玩家二脚本")]
    public PlayerAttack playerTwo;
    [Header("游戏结束")]
    public Text gameOverText;
    [Header("攻击最小值")]
    public int minDamage = 2;
    [Header("攻击最大值")]
    public int maxDamage = 10;

    void Start()
    {
        // 注册按钮点击事件
        playerOneBtn.onClick.AddListener(delegate { OnPlayerOneAttack(); });
        playerTwoBtn.onClick.AddListener(delegate { OnPlayerTwoAttack(); });
    }

    void Update()
    {
        // 判断是否游戏结束
        if (playerOne.IsGameOver || playerTwo.IsGameOver)
        {
            gameOverText.text = "游戏结束!!!";
            // 按钮不可交互
            playerOneBtn.interactable = false;
            playerTwoBtn.interactable = false;
        }
    }

    // 玩家一攻击，玩家二受伤
    public void OnPlayerOneAttack()
    {
        int damage = Random.Range(minDamage, maxDamage);
        playerOne.Attack();
        playerTwo.Damage(damage);
    }

    // 玩家二攻击，玩家一受伤
    public void OnPlayerTwoAttack()
    {
        int damage = Random.Range(minDamage, maxDamage);
        playerTwo.Attack();
        playerOne.Damage(damage);
    }
}
