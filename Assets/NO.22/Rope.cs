using UnityEngine;

public class Rope : MonoBehaviour
{
    private Vector3 centerOfMass = new Vector3(0f, 1.0f, 0f);

    void Start()
    {
        // 设置质心为绳子中心
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
    }
}
