using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    // 正常镜头位置
    private Vector3 cameraPos;
    // 震动镜头位置 
    private Vector3 nextPos;
    // 震动范围
    private float shakingPos = 0.2f;
    // 震动频率
    private float shakingRange = 0.1f;

    void Start()
    {
        // 获取镜头位置
        cameraPos = transform.position;
        // InvokeRepeating(methodName, time, repeatRate)
        // 程序开始time秒后，每经过repeatRate秒就自动调用methodName函数
        InvokeRepeating("Shaking", 0.0f, shakingRange);
    }

    void Shaking()
    {
        // x y z方向初始化为 0
        nextPos = Vector3.zero;
        // 随机取得 x 方向位置 
        nextPos.x = cameraPos.x + Random.Range(-shakingPos, shakingPos);
        // 随机取得 y 方向位置
        nextPos.y = cameraPos.y + Random.Range(-shakingPos, shakingPos);
        // z方向保持不变
        nextPos.z = cameraPos.z;
        // 注意这里不要写成 cameraPos = nextPos
        transform.position = nextPos;
    }
}
