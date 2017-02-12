using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float minDistance = 2.0f;  // 拉近的最小值
    public float maxDistance = 10.0f;  // 拉远的最大值
    public float minAngle = 10.0f;  // 向下的最小值
    public float maxAngle = 80.0f;  // 向上的最大值
    public float scrollSpeed = 3;  // 控制拉近拉远的速度
    public float rotareSpeed = 2;  // 控制左右上下的速度

    private Transform player;
    private Vector3 offsetPosition;  // 初始镜头和玩家的位置偏移量

    private const string playerTag = "Player";

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        offsetPosition = transform.position - player.position;  // 初始化偏移量
    }

    void Update()
    {
        transform.position = offsetPosition + player.position;  // 镜头跟随玩家

        ScrollView();  // 控制镜头的拉近拉远
        RotateView();  // 控制镜头的左右上下
    }

    // 控制镜头的拉近拉远
    void ScrollView()
    {
        /*
            Vector.magnitude
                返回向量的长度，向量的长度是(x*x+y*y+z*z)的平方根。
            Input.GetAxis("Mouse ScrollWheel")
                鼠标向后滑动返回负数（拉近视野），向前滑动正数（拉远视野）
            Mathf.Clamp(float value, float min, float max)
                限制value的值在min和max之间，如果value小于min，返回min。如果value大于max，返回max。否则返回value。
        */
        float distance = offsetPosition.magnitude;
        distance += Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        offsetPosition = offsetPosition.normalized * distance;  // 改变位置便移
    }


    // 控制镜头的左右上下
    void RotateView()
    {
        /*
            Input.GetMouseButton(1)
                得到鼠标右键的按下
            Input.GetAxis("Mouse X")
                得到鼠标水平方向的滑动
            Input.GetAxis("Mouse Y")
                得到鼠标垂直方向的滑动
            Transform.RotateAround(Vector3 point, Vector3 axis, float angle)
                一个物体围绕 point位置 的 axis轴 旋转 angle角度
        */
        if (Input.GetMouseButton(1))
        {
            // 左右
            transform.RotateAround(player.position, player.up, rotareSpeed * Input.GetAxis("Mouse X"));

            Vector3 originalPos = transform.position;  // 记录镜头位置
            Quaternion originalRotation = transform.rotation;  // 记录镜头旋转

            // 上下 (会影响到的属性一个是Position，一个是Rotation)
            transform.RotateAround(player.position, transform.right, -rotareSpeed * Input.GetAxis("Mouse Y"));
            float x = transform.eulerAngles.x;
            if (x < minAngle || x > maxAngle)  // 限制上下范围
            {
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }

            offsetPosition = transform.position - player.position;
        }
    }
}
