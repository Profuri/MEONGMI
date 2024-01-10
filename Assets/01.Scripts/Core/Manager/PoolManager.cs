using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager{
    public static PoolManager Instance;

    private Dictionary<string,Pool<PoolableMono>> _pools = new Dictionary<string, Pool<PoolableMono>>();

    private Transform _trmParent;

    public PoolManager(Transform parent){
        _trmParent = parent;
    }

    public void CreatePool(PoolableMono prefab,int count = 10)
    {
        var parent = _trmParent;

        if (prefab is UIComponent)
        {
            parent = UIManager.Instance.MainCanvas.transform;
        }

        if (!_pools.ContainsKey(prefab.gameObject.name))
        {
            Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab,parent,count);
            _pools.Add(prefab.gameObject.name,pool);
        }
    }

    public PoolableMono Pop(string prefabName){
        if(!_pools.ContainsKey(prefabName)){
            Debug.LogError($"Prefab does not exist on pool: {prefabName}");
            return null;
        }
        PoolableMono item = _pools[prefabName].Pop();
        item.Init();
        return item;
    }

    public void Push(PoolableMono obj){
        _pools[obj.name].Push(obj);
    }
}
