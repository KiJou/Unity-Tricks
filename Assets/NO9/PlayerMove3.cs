using UnityEngine;

public class PlayerMove3 : MonoBehaviour
{
    public float speed = 2.0f;
    private JoyStick js;

    void Start()
    {
        js = GameObject.FindObjectOfType<JoyStick>();
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
        Debug.Log("正在移动虚拟摇杆");
        // 角色朝向
        transform.rotation = Quaternion.LookRotation(new Vector3(vec.x, 0, vec.y));
        // 角色移动
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnJoyStickEnd()
    {
        Debug.Log("触摸移动摇杆结束");
    }

    void OnGUI()
    {
        GUI.Label(new Rect(30, 30, 200, 30), "3D模式下的虚拟摇杆测试");
    }
}