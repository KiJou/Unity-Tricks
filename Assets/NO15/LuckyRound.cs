using UnityEngine;
using UnityEngine.UI;

public class LuckyRound : MonoBehaviour
{
    public Text nameText;
    public Image round1Image;
    public Image round2Image;
    public Button goBtn;
    public Transform pointTransform;

    private bool isStart = false;
    private bool isPause = false;

    // 指针缓慢速度（每帧0.5度）
    private float deltaSpeed = 0.5f;
    // 指针旋转速度（一秒360度）
    private float rotateSpeed = 360.0f;

    // 获奖物品列表
    private string[] switchNames = { "金钱", "装备", "碎片", "VIP", "材料", "英雄", "钻石", "经验" };

    // Round1 <=> Round2
    private int switchRound = 0;
    // 灯泡闪烁速度（0.1秒一次）
    private float switchRoundTime = 0.1f;

    void Start()
    {
        goBtn.onClick.AddListener(delegate { OnStart(); });
    }

    void Update()
    {
        if (isStart && !isPause)
        {
            // 指针顺时针旋
            pointTransform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
            // 指针旋转速度缓慢
            rotateSpeed -= deltaSpeed;

            
            // 360 为旋转一圈度数，22.5f 为初始偏移度数，取余 360 防止下标越界
            float angle = (360 - pointTransform.eulerAngles.z + 22.5f) % 360;
            // 每个奖品区域 45 度
            int index = (int)angle / 45;
            // 根据角度计算决定奖品
            nameText.text = switchNames[index];

            // 判断指针是否停止
            if (rotateSpeed <= 0)
            {
                isPause = true;
                // 取消Invoke
                CancelInvoke();
            }
        }
    }

    void OnStart()
    {
        isStart = true;
        // 视觉上产生动态效果
        InvokeRepeating("SwitchRound", 0.0f, switchRoundTime);
    }

    void SwitchRound()
    {
        // 切换到第一张图，隐藏第二张图
        if (switchRound == 1)
        {
            round1Image.enabled = true;
            round2Image.enabled = false;
            switchRound = 0;
        }
        // 切换到第二张图，隐藏第一张图
        else
        {
            round1Image.enabled = false;
            round2Image.enabled = true;
            switchRound = 1;
        }
    }
}
