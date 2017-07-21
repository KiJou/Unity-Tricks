using UnityEngine;

public class Detecting : MonoBehaviour
{
    [Header("怪物目标")]
    public Transform target;
    [Header("最远距离")]
    public float maxDistance = 10.0f;
    [Header("最大角度")]
    public float maxAngle = 60.0f;

    void Update()
    {
        // 画出大致范围线条
        Draw();

        // 玩家位置
        Vector3 pos = transform.position;
        // 怪物位置
        Vector3 tarPos = target.position;
        // 玩家正方向
        Quaternion rot = transform.rotation;

        // 计算距离
        float distance = Vector3.Distance(pos, tarPos);
        // 玩家法线
        Vector3 normal = pos + rot * Vector3.forward * maxDistance;
        // 怪物到玩家的方向 
        Vector3 offset = tarPos - pos;

        /*
            Vector3.normalized
                向量标准化。
            Vector3.Dot(Vector3 lhs, Vector3 rhs)
                返回两个向量的点乘积。
            Mathf.Rad2Deg
                弧度到度的转化常量。
            Mathf.Acos(float f)
                以弧度为单位计算并返回参数f中指定的数字的反余弦值。
        */
        float angle = Mathf.Acos(Vector3.Dot(normal.normalized, offset.normalized)) * Mathf.Rad2Deg;

        // 判断是否在范围之内
        if (distance <= maxDistance && angle <= maxAngle / 2)
        {
            Debug.Log("怪物在范围内...");
        }
        else
        {
            Debug.Log("怪物不在范围内...");
        }
    }

    void Draw()
    {
        // 玩家位置
        Vector3 pos = transform.position;
        // 玩家方向
        Quaternion rot = transform.rotation;
        // 正方向可视最远点
        Vector3 frontPos = pos + rot * Vector3.forward * maxDistance;
        // 正偏左方向
        Quaternion left = Quaternion.Euler(rot.eulerAngles.x,
                                            rot.eulerAngles.y - maxAngle / 2,
                                            rot.eulerAngles.z);
        // 正偏右方向
        Quaternion right = Quaternion.Euler(rot.eulerAngles.x,
                                            rot.eulerAngles.y + maxAngle / 2,
                                            rot.eulerAngles.z);
        // 正偏左方向可视最远点
        Vector3 leftPos = pos + left * Vector3.forward * maxDistance;
        // 正偏右方向可视最远点 
        Vector3 rightPos = pos + right * Vector3.forward * maxDistance;

        // 将玩家可视范围画出来
        Debug.DrawLine(pos, frontPos, Color.red);
        Debug.DrawLine(pos, rightPos, Color.red);
        Debug.DrawLine(pos, leftPos, Color.red);
        Debug.DrawLine(leftPos, frontPos, Color.red);
        Debug.DrawLine(rightPos, frontPos, Color.red);
    }
}
