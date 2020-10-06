using System.Collections.Generic;
using UnityEngine;

public class PoolMgr : UnitySingleton<PoolMgr>
{
    [Header("物体1")]
    public GameObject obj1;
    [Header("物体2")]
    public GameObject obj2;

    public const string OBJ1_POOL = "obj1";
    public const string OBJ2_POOL = "obj2";

    private Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();
    public override void Awake() { base.Awake(); Init(); }

    void Init()
    {
        // 物体1
        Pool obj1Pool = new Pool(OBJ1_POOL, 100, obj1);
        poolDict.Add(OBJ1_POOL, obj1Pool);
        // 物体2
        Pool obj2Pool = new Pool(OBJ2_POOL, 100, obj2);
        poolDict.Add(OBJ2_POOL, obj2Pool);
    }

    // 从对象池获取物体
    public GameObject Spawn(string poolName)
    {
        Pool pool;
        poolDict.TryGetValue(poolName, out pool);
        if (pool != null)
        {
            return pool.Spawn();
        }
        else
        {
            Debug.LogError(poolName + " Error");
            return null;
        }
    }

    // 将物体放回对象池
    public void Unspawn(GameObject go)
    {
        Pool pool;
        poolDict.TryGetValue(go.name, out pool);
        if (pool != null)
        {
            pool.Unspawn(go);
        }
        else
        {
            Debug.LogError(go.name + " Error");
        }
    }
}
