using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour, ISpawnerWithStats
    where T : MonoBehaviour, ITimeoutable<T>
{
    [SerializeField] private int _initialCount;
    [Space]
    [SerializeField] protected T _prefab;

    public event Action<int, int> Spawned;

    private int _objCount = 0;
    private int _totalSpawnObject = 0;

    private Pool<T> _pool;

    private void Awake()
    {
        _pool = new Pool<T>(_prefab, _initialCount);
    }

    protected T SpawnObject(Vector3 position)
    {
        T obj = _pool.Get();
        obj.transform.SetParent(gameObject.transform);
        obj.TimeOut += OnObjectTimeout;
        obj.gameObject.transform.position = position;
        obj.gameObject.SetActive(true);

        _objCount++;
        _totalSpawnObject++;
        Spawned?.Invoke(_objCount, _totalSpawnObject);

        return obj;
    }

    protected abstract void OnObjectReleased(T obj);

    private void OnObjectTimeout(T obj)
    {
        obj.TimeOut -= OnObjectTimeout;
        OnObjectReleased(obj);

        _objCount--;
        Spawned?.Invoke(_objCount, _totalSpawnObject);
        _pool.Release(obj);
    }
}
