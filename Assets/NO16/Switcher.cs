using UnityEngine;

public class Switcher : MonoBehaviour
{
    private Vector3 Center = Vector3.zero;
    private float offsetX = 0.6f;
    private float offsetY = 0.1f;
    private float offsetZ = 0.1f;

    private int halfSize;
    private GameObject[] sprites;

    private int index;
    private Rect rect = new Rect(300, 200, 100, 100);  // x y width height

    void Start()
    {
        // 初始化精灵数组
        int childCount = transform.childCount;
        // 计算两侧精灵数目
        halfSize = (childCount - 1) / 2;
        // 初始化精灵
        sprites = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            sprites[i] = transform.GetChild(i).gameObject;
            SetPosition(i);
            // SetDeepin(i);
        }
        index = halfSize + 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OnPre();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            OnNext();
        }
    }

    // 计算精灵坐标
    void SetPosition(int index)
    {
        float x;
        float y;
        float z;
        if (index < halfSize)
        {
            x = -(halfSize - index) * offsetX + Center.x;
            y = (halfSize - index) * offsetY + Center.y;
            z = (halfSize - index) * offsetZ + Center.z;
        }
        else if (index > halfSize)
        {
            x = (index - halfSize) * offsetX + Center.x;
            y = (index - halfSize) * offsetY + Center.y;
            z = (index - halfSize) * offsetZ + Center.z;
        }
        else
        {
            x = Center.x;
            y = Center.y;
            z = Center.z;
        }
        sprites[index].GetComponent<Transform>().position = new Vector3(x, y, z);
    }

    // 设置精灵深度
    private void SetDeepin(int index)
    {
        //计算精灵深度
        int deepin = 0;
        if (index < halfSize)
        {
            deepin = index;
        }
        else if (index > halfSize)
        {
            deepin = sprites.Length - (1 + index);
        }
        else
        {
            deepin = halfSize;
        }
        sprites[index].GetComponent<Transform>().SetSiblingIndex(deepin);
    }

    void OnNext()
    {
        int length = sprites.Length;

        // 记录最后一张卡牌
        Sprite temp = sprites[length - 1].GetComponent<SpriteRenderer>().sprite;
        for (int i = length - 1; i > 0; --i)
        {
            sprites[i].GetComponent<SpriteRenderer>().sprite = sprites[i - 1].GetComponent<SpriteRenderer>().sprite;
        }
        // 重新赋值
        sprites[0].GetComponent<SpriteRenderer>().sprite = temp;

        index++;
        if (index > sprites.Length)
        {
            index = 1;
        }
    }

    void OnPre()
    {
        int length = sprites.Length;

        // 记录第一张卡牌
        Sprite temp = sprites[0].GetComponent<SpriteRenderer>().sprite;
        for (int i = 0; i < length - 1; ++i)
        {
            sprites[i].GetComponent<SpriteRenderer>().sprite = sprites[i + 1].GetComponent<SpriteRenderer>().sprite;
        }
        // 重新赋值
        sprites[length - 1].GetComponent<SpriteRenderer>().sprite = temp;

        index--;
        if (index < 1)
        {
            index = sprites.Length;
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = new Color(1, 0, 0);  // 字体颜色
        style.fontSize = 40;  // 字体大小
        GUI.Label(rect, index + " / " + sprites.Length, style);
    }
}
