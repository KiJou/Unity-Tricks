using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    [Header("释放按键")]
    public KeyCode key;
    [Header("冷却时间")]
    public float cd;

    // 冷却Image
    private Image cdImg;
    // 冷却Text
    private Text cdText;
    // 冷却计时
    private float timer = 0;
    // 冷却完毕
    private bool isOk = true;

    void Start()
    {
        cdImg = transform.Find("CDImg").GetComponent<Image>();
        cdText = transform.Find("CDText").GetComponent<Text>();
        cdImg.enabled = false;
        cdText.enabled = false;
    }

    void Update()
    {
        if (!isOk)
        {
            // 冷却计时
            timer -= Time.deltaTime;
            // 冷却Image
            cdImg.fillAmount = timer / cd;
            // 冷却Text
            cdText.text = Mathf.Ceil(timer).ToString();

            if (timer <= 0)
            {
                // 完成冷却
                isOk = true;
                // 隐藏UI
                cdImg.enabled = false;
                cdText.enabled = false;
            }
        }

        if (isOk)
        {
            if (Input.GetKeyDown(key))
            {
                // TODO
                Debug.Log("释放技能 " + key);

                // 重新冷却
                isOk = false;
                timer = cd;
                // 显示UI
                cdImg.enabled = true;
                cdText.enabled = true;
            }
        }
    }
}
