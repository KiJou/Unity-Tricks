using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{

    void Update()
    {
        // 左键
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = PoolMgr.Instance.Spawn(PoolMgr.OBJ1_POOL);
            obj.transform.SetParent(this.transform);
            obj.transform.position = new Vector3(0, 5, 0);
            StartCoroutine(Unspawn(obj, 5.0f));
        }
        // 右键
        else if (Input.GetMouseButtonDown(1))
        {
            GameObject obj = PoolMgr.Instance.Spawn(PoolMgr.OBJ2_POOL);
            obj.transform.SetParent(this.transform);
            obj.transform.position = new Vector3(0, 5, 0);
            StartCoroutine(Unspawn(obj, 5.0f));
        }
    }

    IEnumerator Unspawn(GameObject obj, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        PoolMgr.Instance.Unspawn(obj);
    }
}
