using UnityEngine;

public class LoadScene : MonoBehaviour
{

    void Update()
    {
        // 加载第一个场景
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneMgr.Instance.LoadScene(0);
        }
        // 加载第二个场景
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SceneMgr.Instance.LoadScene(1);
        }
    }
}
