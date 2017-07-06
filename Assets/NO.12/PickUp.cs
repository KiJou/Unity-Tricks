using UnityEngine;

public class PickUp : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 生成射线
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            // 射线数据
            RaycastHit hit;
            // 如果射线碰撞到对象，把返回数据储存到hit中
            if (Physics.Raycast(ray, out hit))
            {
                // 销毁碰撞到的物品
                if (hit.transform.CompareTag("Item"))
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
