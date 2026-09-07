using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Pool<T> where T : MonoBehaviour
{
    private int _createCount = 0;
    private T _prefab;
    private Stack<T> _objects = new();

    public int CreateCount => _createCount;

    public Pool(T prefab, int initialCount)
    {
        _prefab = prefab;

        for (int i = 0; i < initialCount; i++)
        {
            Create();
        }
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        _objects.Push(obj);
    }

    public T Get()
    {
        if(_objects.Count == 0)
        {
            Create();
        }

        return _objects.Pop();
    }

    private void Create()
    {
        var obj = Object.Instantiate(_prefab);
        _createCount++;
        Release(obj);
    }
}
