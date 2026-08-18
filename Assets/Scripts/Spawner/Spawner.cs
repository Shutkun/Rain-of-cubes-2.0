using System.Collections.Generic;
using UnityEngine;

public class Spawners<T> where T : MonoBehaviour
{
    private Stack<T> _objects = new();
    private T _prefab;

    public Spawners(T prefab, int maxSize)
    {
        _prefab = prefab;

        for (int i = 0; i < maxSize; i++)
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
        Release(obj);
    }
}
