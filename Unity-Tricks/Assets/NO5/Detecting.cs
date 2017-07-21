using UnityEngine;

public class Detecting : MonoBehaviour
{
    public Transform target;  // 怪物目标

    public float maxDistance = 5.0f;  // 最远距离
    public float maxAngle = 60.0f;  // 最大角度

    void Update()
    {
        /*  这个方法可行，但是复杂而且误差
        Vector3 pos = transform.position;  // 玩家位置
        Quaternion rot = transform.rotation;  // 玩家正方向

        Vector3 frontPos = pos + rot * Vector3.forward * maxDistance;  // 正方向可视最远点

        Quaternion left = Quaternion.Euler(rot.eulerAngles.x,
                                            rot.eulerAngles.y - maxAngle / 2,
                                            rot.eulerAngles.z);  // 正偏左方向
        Quaternion right = Quaternion.Euler(rot.eulerAngles.x,
                                            rot.eulerAngles.y + maxAngle / 2,
                                            rot.eulerAngles.z);  // 正偏右方向

        Vector3 leftPos = pos + left * Vector3.forward * maxDistance;  // 正偏左方向可视最远点
        Vector3 rightPos = pos + right * Vector3.forward * maxDistance;  // 正偏右方向可视最远点

        // 将玩家可视范围画出来
        Debug.DrawLine(pos, frontPos, Color.red);
        Debug.DrawLine(pos, rightPos, Color.red);
        Debug.DrawLine(pos, leftPos, Color.red);
        Debug.DrawLine(leftPos, frontPos, Color.red);
        Debug.DrawLine(rightPos, frontPos, Color.red);
        */

        Vector3 pos = transform.position;  // 玩家位置
        Vector3 tarPos = target.position;  // 怪物位置
        Quaternion rot = transform.rotation;  // 玩家正方向

        // 计算距离
        float distance = Vector3.Distance(pos, tarPos);

        Vector3 normal = pos + rot * Vector3.forward * maxDistance;  // 玩家法线
        Vector3 offset = tarPos - pos;  // 怪物到玩家的方向
        Debug.DrawLine(pos, normal, Color.red);
        Debug.DrawLine(pos, tarPos, Color.red);

        // 计算夹角
        float angle = Mathf.Acos(Vector3.Dot(normal.normalized, offset.normalized)) * Mathf.Rad2Deg;

        if (distance <= maxDistance && angle <= maxAngle / 2)  // 判断是否在范围之内
        {
            Debug.Log("怪物在范围内...");
        }
        else
        {
            Debug.Log("怪物不在范围内...");
        }
    }
}
