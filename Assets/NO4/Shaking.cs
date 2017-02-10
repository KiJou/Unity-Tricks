using UnityEngine;
using System.Collections;

public class Shaking : MonoBehaviour
{
    private Vector3 cameraPos;  // 正常镜头位置
    private Vector3 nextPos = Vector3.zero;  // 震动镜头位置
    private float shakingPos = 0.5f;  // 震动范围
    private float shakingRange = 0.1f;  // 震动频率

    void Start()
    {
        cameraPos = this.transform.position;  // 获取镜头位置
        // InvokeRepeating(methodName, time, repeatRate)
        // 程序开始 time 秒后，每经过 repeatRate 秒就自动调用 methodName 函数
        InvokeRepeating("DelayShaking", 0.0f, shakingRange);  // 每经过 repeatTime 就自动震动
    }

    void DelayShaking()
    {
        nextPos = Vector3.zero;  // x y z 方向初始化为 0
        nextPos.x = cameraPos.x + Random.Range(-shakingRange, shakingRange);  // 随机取得 x 方向位置
        nextPos.y = cameraPos.y + Random.Range(-shakingRange, shakingRange);  // 随机取得 y 方向位置
        nextPos.z = cameraPos.z;  // z 方向保持不变
        this.transform.position = nextPos;  // 注意这里不要写成 cameraPos = nextPos
    }
}
