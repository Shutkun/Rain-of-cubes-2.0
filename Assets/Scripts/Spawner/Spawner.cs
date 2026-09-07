using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour, ISpawnerWithStats
    where T : MonoBehaviour, ITimeoutable<T>
{
    [SerializeField] private int _initialCount;
    [Space]
    [SerializeField] protected T Prefab;

    private int _objCount = 0;
    private int _totalSpawnObject = 0;
    private Pool<T> _pool;

    public event Action<int, int, int> Spawned;

    private void Awake()
    {
        _pool = new Pool<T>(Prefab, _initialCount);
    }

    protected T SpawnObject(Vector3 position)
    {
        T obj = _pool.Get();
        obj.transform.SetParent(gameObject.transform);
        obj.TimeOut += Timeout;
        obj.gameObject.transform.position = position;
        obj.gameObject.SetActive(true);

        _objCount++;
        _totalSpawnObject++;
        Spawned?.Invoke(_objCount, _totalSpawnObject, _pool.CreateCount);

        return obj;
    }

    protected abstract void ReleaseObject(T obj);

    private void Timeout(T obj)
    {
        obj.TimeOut -= Timeout;
        ReleaseObject(obj);

        _objCount--;
        Spawned?.Invoke(_objCount, _totalSpawnObject, _pool.CreateCount);
        _pool.Release(obj);
    }
}
