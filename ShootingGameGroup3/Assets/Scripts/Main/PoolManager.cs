using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<PoolManager>();
            }
            return _instance;
        }
    }

    private Dictionary<string, ObjectPool> _pools = new Dictionary<string, ObjectPool>();

    public void CreatePool(GameObject prefab, int initSize)
    {
        string key = prefab.name;
        if(!_pools.ContainsKey(key))
        {
            _pools.Add(key, new ObjectPool(prefab, initSize, transform));
        }
    }

    public GameObject GetGameObject(GameObject prefab)
    {
        string key = prefab.name;
        if(!_pools.ContainsKey(key))
        {
            CreatePool(prefab, 1);
        }
        return _pools[key].GetGameObject();
    }

    public void ReturnGameObject(GameObject obj)
    {
        string key = obj.name.Replace("(Clone)", "");
        if (_pools.ContainsKey(key))
        {
            _pools[key].ReturnGameObject(obj);
        }
    }
}
