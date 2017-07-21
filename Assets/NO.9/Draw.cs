using UnityEngine;

public class Draw : MonoBehaviour
{
    [Header("线条预制体")]
    public GameObject linePrefab;

    // 线条渲染器
    private LineRenderer line;
    // 顶点下标
    private int i;

    void Update()
    {
        // 鼠标左键按下瞬间
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(linePrefab, linePrefab.transform.position, transform.rotation);
            line = go.GetComponent<LineRenderer>();
            // 设置材质
            line.material = new Material(Shader.Find("Particles/Additive"));
            // 设置颜色
            line.startColor = Color.red;
            line.endColor = Color.red;
            // 设置宽度
            line.startWidth = 0.1f;
            line.endWidth = 0.1f;
            i = 0;
        }
        // 鼠标左键按下期间
        if (Input.GetMouseButton(0))
        {
            i++;
            // 设置顶点数
            line.positionCount = i;
            // 设置顶点位置
            line.SetPosition(i - 1, Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15)));
        }
    }
}
