using UnityEngine;

public class Sphere : MonoBehaviour
{
    private int force = 10;

    void Start()
    {
        /*
            添加一个力到刚体
            Impulse为冲力
        */
        GetComponent<Rigidbody>().AddForce(Vector3.right * force, ForceMode.Impulse);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * force, ForceMode.Impulse);
        }
    }
}
