using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    // 单例模式，全局只有一个，方便外界调用
    private static FoodSpawn _instance;
    public static FoodSpawn Instance { get { return _instance; } }

    public GameObject foodGo;  // 食物
    public float spawnTime = 5.0f;  // 生成食物的时间
    public int bgX = 33;
    public int bgY = 9;

    private int maxX = 16;
    private int maxY = 9;

    void Start()
    {
        _instance = this;
        InvokeRepeating("Spawn", 0.0f, spawnTime);
        /* 
            蛇身长度为1，背景长度宽度都应该是奇数
            1. 上部分 + 中间1格 + 下部分 = 宽度
            2. 左部分 + 中间1格 + 右部分 = 长度
            在上下左右对称的情况下，1和2结果都是奇数
        */
        maxX = bgX / 2;  // 33 / 2 = 16
        maxY = bgY / 2;  // 19 / 2 = 9
    }

    public void Spawn()  // 随机生成食物
    {
        float x = Random.Range(-maxX, maxX);
        float y = Random.Range(-maxY, maxY);
        GameObject go = Instantiate(foodGo);  // 生成食物
        foodGo.transform.position = new Vector3(x, y, 0);  // 初始化位置
        go.transform.SetParent(this.transform);  // 设置父物体
    }
}
