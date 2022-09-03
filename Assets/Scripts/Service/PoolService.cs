using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolService : BaseService<PoolService>
{
    private class PoolData
    {
        private readonly GameObject _fatherObj;
        public readonly Stack<GameObject> Stack;

        public PoolData(Object obj, GameObject root)
        {
            _fatherObj = new GameObject(obj.name);
            _fatherObj.transform.SetParent(root.transform);
            Stack = new Stack<GameObject>();
        }

        public GameObject Get()
        {
            GameObject obj = Stack.Pop();
            obj.SetActive(true);
            return obj;
        }

        public void Release(GameObject obj)
        {
            obj.transform.SetParent(_fatherObj.transform);
            obj.SetActive(false);
            Stack.Push(obj);
        }
    }

    private readonly Dictionary<string, PoolData> _poolDic = new Dictionary<string, PoolData>();

    private GameObject _poolRoot;

    public GameObject Get(string path, UnityAction<GameObject> callBack = null)
    {
        GameObject obj;
        string objName = path[(path.LastIndexOf('/') + 1)..];

        if (_poolDic.ContainsKey(objName) && _poolDic[objName].Stack.Count > 0)
        {
            obj = _poolDic[objName].Get();
            callBack?.Invoke(obj);
        }
        else
        {
            obj = Instantiate(ResourceService.Instance.Load<GameObject>(path));
            obj.name = objName;
            callBack?.Invoke(obj);
        }

        return obj;
    }

    public void Release(GameObject obj)
    {
        string objName = obj.name;
        _poolRoot ??= new GameObject("Pool");
        if (!_poolDic.ContainsKey(objName))
        {
            _poolDic.Add(objName, new PoolData(obj, _poolRoot));
        }

        _poolDic[objName].Release(obj);
    }

    public void Clear()
    {
        _poolDic.Clear();
        _poolRoot = null;
    }
}