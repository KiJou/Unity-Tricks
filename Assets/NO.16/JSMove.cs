using UnityEngine;

public class JSMove : MonoBehaviour
{
    [Header("移动速度")]
    public float speed = 2.0f;
    [Header("虚拟摇杆")]
    public JoyStick js;

    void Start()
    {
        js.OnJoyStickTouchBegin += OnJoyStickBegin;
        js.OnJoyStickTouchMove += OnJoyStickMove;
        js.OnJoyStickTouchEnd += OnJoyStickEnd;
    }

    void OnJoyStickBegin(Vector2 vec)
    {
        Debug.Log("开始触摸虚拟摇杆");
    }

    void OnJoyStickMove(Vector2 vec)
    {
        // 角色方向
        transform.rotation = Quaternion.LookRotation(new Vector3(vec.x, 0, vec.y));
        // 角色移动
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        // Run
        GetComponent<Animation>().CrossFade("Run");
        Debug.Log("正在移动虚拟摇杆");
    }

    void OnJoyStickEnd()
    {
        // Idle
        GetComponent<Animation>().CrossFade("Idle");
        Debug.Log("触摸移动摇杆结束");
    }
}