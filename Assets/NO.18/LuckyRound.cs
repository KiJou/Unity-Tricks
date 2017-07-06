using UnityEngine;
using UnityEngine.UI;

public class LuckyRound : MonoBehaviour
{
    [Header("获奖文字")]
    public Text nameText;
    [Header("灯泡图片")]
    public Image roundImage;
    [Header("开始按钮")]
    public Button goBtn;
    [Header("指针组件")]
    public Transform pointTransform;

    // 指针缓慢速度（每帧0.5度）
    private float deltaSpeed = 0.5f;
    // 指针旋转速度（一秒360度）
    private float rotateSpeed = 360.0f;
    // 灯泡速度（一次0.1秒）
    private float switchRoundTime = 0.1f;
    // 获奖列表
    private string[] switchNames = { "金钱", "装备", "碎片", "VIP", "材料", "英雄", "钻石", "经验" };
    // 是否开始
    private bool isStart = false;
    // 是否停止
    private bool isStop = false;

    void Start()
    {
        goBtn.onClick.AddListener(delegate { OnStart(); });
    }

    void OnStart()
    {
        isStart = true;
        InvokeRepeating("SwitchRound", 0.0f, switchRoundTime);
    }

    void Update()
    {
        if (isStart && !isStop)
        {
            // 指针顺时针旋转
            pointTransform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
            // 指针旋转速度放慢
            rotateSpeed -= deltaSpeed;
            // 360为旋转一圈度数，22.5f为初始偏移度数，取余360防止下标越界
            float angle = (360 - pointTransform.eulerAngles.z + 22.5f) % 360;
            // 每个奖品区域45度
            int index = (int)angle / 45;
            // 根据角度决定奖品
            nameText.text = switchNames[index];
            // 判断指针是否停止
            if (rotateSpeed <= 0)
            {
                isStop = true;
                // 取消Invoke
                CancelInvoke();
            }
        }
    }

    void SwitchRound()
    {
        // 切换灯泡图片
        roundImage.enabled = !roundImage.enabled;
    }
}
