using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 4.0f;  // 玩家移动速度

    private Rigidbody mRigidbody;  // 玩家刚体，用于控制玩家移动
    private float min = 0.05f;  // 最小移动量

    void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");  // 获取水平轴移动量
        float v = Input.GetAxis("Vertical");  // 获取垂直轴移动量

        if(Mathf.Abs(h) >= min || Mathf.Abs(v) >= min)  // 超过最小移动量才允许玩家移动
        {
            Vector3 targetPos = new Vector3(h, 0, v);  // 目标方向

            mRigidbody.velocity = targetPos * speed;  // 改变速度
            transform.rotation = Quaternion.LookRotation(targetPos);  // 改变朝向
        }
        else
        {
            mRigidbody.velocity = Vector3.zero;  // 停止移动
        }
    }
}
