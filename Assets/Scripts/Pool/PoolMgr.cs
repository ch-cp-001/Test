using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 缓存池的栈数据容器
/// </summary>
public class PoolStackData
{
    // 存储未使用的对象数据  
    private Stack<GameObject> poolStack;
    // 存储使用中的对象数据
    private List<GameObject> useList;
    // 优化编辑器布局使用
    private GameObject poolStackObj;

    public int Count => poolStack.Count;
    public int useCount => useList.Count;
    //最大容量
    public int maxNum { get; }

    public PoolStackData(GameObject parent,string name,int maxNum)
    {
        poolStack = new Stack<GameObject>();
        useList = new List<GameObject>();
        this.maxNum = maxNum;

        if (PoolMgr.isLayout)
        {
            poolStackObj = new GameObject();
            poolStackObj.name = name;
            poolStackObj.transform.SetParent(parent.transform);
        }
    }

    // 从数据栈中获取对象
    public GameObject Pop()
    {
        GameObject obj = poolStack.Pop();
        obj.SetActive(true);
        Add(obj);
        if (PoolMgr.isLayout) obj.transform.SetParent(null);
        return obj;
    }

    // 向未使用数据栈中存储对象
    public void Push(GameObject obj)
    {
        poolStack.Push(obj);
        obj.SetActive(false);
        if (PoolMgr.isLayout) obj.transform.SetParent(poolStackObj.transform);
    }

    // 从使用中容器移除对象
    public GameObject RemoveOldest()
    {
        GameObject obj = useList[0];
        useList.RemoveAt(0);
        return obj;
    }

    public void Remove(GameObject obj)
    {
        useList.Remove(obj);
    }

    // 向使用中的容器存储对象
    public void Add(GameObject obj)
    {
        useList.Add(obj);
    }
}


/// <summary>
/// 缓存池
/// </summary>
public class PoolMgr:SingletonBase<PoolMgr>
{
    private PoolMgr() { }

    private Dictionary<string, PoolStackData> pool = new Dictionary<string, PoolStackData>();

    // 优化编辑器布局使用
    private GameObject poolObj;
    // 控制是否开始优化编辑器布局，发布时设置为false
    public static bool isLayout = false;

    //获取对象方法 没有就创建，有就直接用
    public GameObject GetObj(string name)
    {
        if (poolObj == null && isLayout)
        {
            poolObj = new GameObject();
            poolObj.name = "Pool";
        }

        #region 不设置数量上限时的逻辑
        //if (pool.ContainsKey(name) && pool[name].Count > 0)
        //{
        //    return pool[name].Pop();
        //}
        //else
        //{
        //    GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
        //    // 防止名字不一致导致异常
        //    obj.name = name;
        //    return obj;
        //}
        #endregion

        #region 设置数量上限时的逻辑
        if (!pool.ContainsKey(name))
        {
            GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            // 防止名字不一致导致异常
            obj.name = name;
            UsePoolObj usePoolObj = obj.GetComponent<UsePoolObj>();
            if (usePoolObj == null)
            {
                Debug.LogError("请在要使用缓存池的预设体对象上挂UsePoolObj脚本并设置最大容量");
                return null;
            }
            pool.Add(name,new PoolStackData(poolObj,name, usePoolObj.maxNum));
            pool[name].Add(obj);
            return obj;
        }
        else if (pool[name].Count > 0)
        {
            GameObject obj = pool[name].Pop();
            return obj;
        }
        else if (pool[name].useCount < pool[name].maxNum)
        {
            GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            // 防止名字不一致导致异常
            obj.name = name;
            pool[name].Add(obj);
            return obj;
        }
        else
        {
            GameObject obj = pool[name].RemoveOldest();
            pool[name].Add(obj);
            return obj;
        }
        #endregion
    }

    // 抛弃对象的方法 有对应的栈空间就直接放，否则先创建再放
    public void PushObj(GameObject obj)
    {
        pool[obj.name].Remove(obj);
        pool[obj.name].Push(obj);

    }

    //清空缓存池  过场景时调用，防止内存泄露
    public void ClearPool()
    {
        pool.Clear();
        poolObj = null;
    }

}
