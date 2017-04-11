using UnityEngine;

public class LoadScene : MonoBehaviour
{

    void Update()
    {
        // 加载第一个场景
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneMgr.Instance.LoadScene(0);
        }
        // 加载第二个场景
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneMgr.Instance.LoadScene(1);
        }
    }
}
