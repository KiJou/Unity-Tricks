using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;  // 用来获取 List 的 Last()

public class SnakeMove : MonoBehaviour
{
    public GameObject snakeGo;  // 蛇身部分
    public Transform snakeContainer;  // 蛇身父物体
    public Vector2 direction = Vector2.right;  // 默认开始向右
    public float movingTime = 0.1f;  // 每次移动的时间
    public float movingDistance = 1.0f;  // 每次移动的距离

    private List<Transform> snakeBody = new List<Transform>();  // 蛇身列表（注意蛇头不存在这里）

    private const string snakeTag = "Snake";
    private const string foodTag = "Food";
    private const string wallTag = "Wall";

    void Start()
    {
        InvokeRepeating("Move", 0.0f, movingTime);  // 每经过 movingTime 移动一次
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))  // W 上
        {
            direction = Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))  // S 下
        {
            direction = Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))  // A 左
        {
            direction = Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))  // D 右
        {
            direction = Vector2.right;
        }
    }

    void Move()  // 朝着方向移动
    {
        // 记录当前蛇头位置（此时尚未发生移动）
        Vector3 headPos = transform.position;
        // 蛇头向前移动（注意这个脚本只存在蛇头，蛇身是没有的，所以往前的只有蛇头）
        transform.Translate(direction * movingDistance);

        /*
            最重要的：算法核心！！！
            蛇头向前移动后，整条蛇可能是 [    （蛇尾）口口口口 ? 口（蛇头）    ] 即原来蛇头的位置空缺了。
            我们可以把蛇尾 移动 到这个空缺的位置，即蛇尾不断地 补到 蛇头原来的位置。
        */
        if (snakeBody.Count > 0)
        {
            snakeBody.Last().position = headPos;  // 蛇尾补到空缺的位置
            snakeBody.Insert(0, snakeBody.Last());  // 蛇尾插入蛇身列表第一个位置
            snakeBody.RemoveAt(snakeBody.Count - 1);  // 将原来的蛇尾从蛇身列表移除
        }
    }

    void OnTriggerEnter(Collider other)  // Collider 碰撞检测
    {
        // Debug.Log("I am coming!!!");
        if (other.gameObject.CompareTag(foodTag))  // 碰撞到食物
        {
            // Debug.Log("I am eating you!!!");
            BigBigBig();  // 蛇身变长
            Destroy(other.gameObject);
            // 这里可以优化成使用对象池
            // 生成 setActive true
            // 销毁 setActive false
            // TODO 有心情可以做做......

            FoodSpawn.Instance.Spawn();  // 重新生成食物
        }
        else if (other.gameObject.CompareTag(snakeTag) ||
            other.gameObject.CompareTag(wallTag))  // 碰撞到自己或者墙壁
        {
            CancelInvoke();  // 取消蛇移动
            FoodSpawn.Instance.CancelInvoke();  // 取消食物生成

            SceneManager.LoadScene(0);  // 重新开始游戏
        }
    }

    void BigBigBig()  // 蛇身变长
    {
        GameObject go = Instantiate(snakeGo);
        go.transform.SetParent(snakeContainer);
        snakeBody.Insert(0, go.transform);  // 插到蛇身头部
    }
}
