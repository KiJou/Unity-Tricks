using UnityEngine;

public class AudioTest : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioMgr.Instance.PlayEffect("click");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            AudioMgr.Instance.PlayMusic("music");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AudioMgr.Instance.PlayMusic("music2");
        }
    }
}
