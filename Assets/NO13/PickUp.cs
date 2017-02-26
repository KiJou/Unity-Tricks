using UnityEngine;

public class PickUp : MonoBehaviour
{
    private string cubeTag = "Cube";

    void Update()
    {
        // 检测鼠标左键的按下
        if (Input.GetMouseButtonDown(0))
        {
            // 创建一条射线，产生的射线是在世界空间中，从相机的近裁剪面开始并穿过屏幕 position(x,y) 像素坐标
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            // RaycastHit 是一个结构体对象，用来储存射线返回的信息
            RaycastHit hit;

            // 如果射线碰撞到对象，把返回信息储存到 hit 中
            if (Physics.Raycast(ray, out hit))
            {
                // 销毁碰撞到的物品
                if (hit.transform.CompareTag(cubeTag))
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
