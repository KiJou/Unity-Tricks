using UnityEngine;
using UnityEngine.UI;

public class DayNight : MonoBehaviour
{
    // 显示时间UI
    public Text timeText;

    // 一天24小时
    private float day = 24.0f;
    // 当前时间
    private float now = 6.0f;
    // 每小时移动角度
    private float perDegree = 15.0f;
    // 初始偏移角度
    private float offsetDegree = 90.0f;

    /*
        日夜交替只需要改变Light角度
        每小时移动角度 = 1 / 24 * 360度 = 15度
        初始偏移角度 = 6 * 15度 = 90度
        6:00 ==> Ligth X轴 0度
        12:00 ==> Ligth X轴 90度
        18:00 ==> Ligth X轴 180度
    */
    void Update()
    {
        // 计算当前时间
        now = (now + Time.deltaTime) % day;
        // 计算光照角度
        float degree = now * perDegree - offsetDegree;
        // 改变Light角度
        transform.rotation = Quaternion.Euler(degree, 0, 0);

        // 向下取整
        timeText.text = "当前时间 : " + Mathf.Floor(now).ToString();
    }
}
