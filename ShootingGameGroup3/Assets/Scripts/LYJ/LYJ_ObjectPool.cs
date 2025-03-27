using System.Collections.Generic;
using UnityEngine;

public class LYJ_ObjectPool : MonoBehaviour
{
    private GameObject _prefab;
    private Queue<GameObject> _pool;

    private Transform _parent;

    public LYJ_ObjectPool(GameObject prefab, int initSize, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;
        
        _pool = new Queue<GameObject>();

        for (int i = 0; i < initSize; ++i)
        {
            CreateNewObject();
        }
    }

    private void CreateNewObject()
    {
        GameObject obj = Instantiate(_prefab, _parent);
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
    

    public GameObject GetGameObject()
    {
        if(_pool.Count == 0)
        {
            CreateNewObject();
        }

        GameObject obj  = _pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnGameObject(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
}
