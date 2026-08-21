using System;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private int _initialCount;
    [Space]
    [SerializeField] private Bomb _bombsPrefab;
    [SerializeField] private CubeSpawner _cubeSpawner;

    public event Action<int, int> BombSpawned;
    public event Action<Bomb> BombReleased;

    private int _objCount = 0;
    private int _totalSpawnObject = 0;

    private Spawners<Bomb> _spawner;

    private void OnEnable()
    {
        _spawner = new Spawners<Bomb>(_bombsPrefab, _initialCount);
        _cubeSpawner.CubRealeased += Spawn;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubRealeased -= Spawn;
    }

    public void Spawn(Transform transform)
    {
        Bomb bomb = _spawner.Get();
        bomb.transform.SetParent(gameObject.transform);
        bomb.TimeOut += ActionOnRelease;
        bomb.gameObject.transform.position = transform.position;
        bomb.gameObject.SetActive(true);
        _objCount++;
        _totalSpawnObject++;
        BombSpawned?.Invoke(_objCount, _totalSpawnObject);
    }

    private void ActionOnRelease(Bomb bomb)
    {
        bomb.TimeOut -= ActionOnRelease;
        BombReleased?.Invoke(bomb);
        _objCount--;
        BombSpawned?.Invoke(_objCount, _totalSpawnObject);
        _spawner.Release(bomb);
    }
}
