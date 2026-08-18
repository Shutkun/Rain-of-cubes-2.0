using System;
using UnityEngine;

public class SpawnerBombs : MonoBehaviour
{
    [SerializeField] private int _poolMaxSize;
    [Space]
    [SerializeField] private Bomb _bombsPrefab;
    [SerializeField] private Cube _parentPrefab;

    public event Action<int, int> BombSpawned;

    private int _objCount = 0;
    private int _totalSpawnObject = 0;

    private Spawners<Bomb> _spawner;

    private void Start()
    {
        _spawner = new Spawners<Bomb>(_bombsPrefab, _poolMaxSize);
    }

    private void OnEnable()
    {
        _parentPrefab.TimeOut += Spawn;
    }

    private void OnDisable()
    {
        _parentPrefab.TimeOut -= Spawn;
    }

    private void Spawn(Cube cube)
    {
        Bomb bomb = _spawner.Get();
        bomb.TimeOut += ActionOnRelease;
        bomb.gameObject.transform.position = cube.transform.position;
        bomb.gameObject.SetActive(true);
        _objCount++;
        _totalSpawnObject++;
        BombSpawned?.Invoke(_objCount, _totalSpawnObject);
    }

    private void ActionOnRelease(Bomb bomb)
    {
        bomb.TimeOut -= ActionOnRelease;
        _objCount--;
        BombSpawned?.Invoke(_objCount, _totalSpawnObject);
        _spawner.Release(bomb);
    }

}
