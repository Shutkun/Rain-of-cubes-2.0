using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private int _initialCount;

    public event Action<int,int> CubsSpawned;
    public event Action<Transform> CubRealeased;
    private float _timeOfWaiting = 0.5f;
    private int _objCount = 0;
    private int _totalSpawnObject = 0;
    private Coroutine _coroutine;
    private Spawners<Cube> _spawner;

    private void Awake()
    {
        _spawner = new Spawners<Cube>(_prefab, _initialCount);
    }

    private void Start()
    {
        _coroutine = StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds _waitForSeconds = new WaitForSeconds(_timeOfWaiting);

       while(enabled)
        {
            Cube cube = _spawner.Get();
            cube.transform.SetParent(transform);
            cube.TimeOut += ActionOnRelease;
            cube.gameObject.transform.position = GetRandomPosition();
            cube.gameObject.SetActive(true);
            _objCount++;
            _totalSpawnObject++;
            CubsSpawned?.Invoke(_objCount,_totalSpawnObject);
            yield return _waitForSeconds;
        }
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.TimeOut -= ActionOnRelease;
        CubRealeased?.Invoke(cube.transform);
        _objCount--;
        CubsSpawned?.Invoke(_objCount,_totalSpawnObject);
        _spawner.Release(cube);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 origin = _startPoint.position;
        Vector3 range = _startPoint.localScale / 2f;
        Vector3 randomRange = new Vector3(
            Random.Range(-range.x, range.x),
            Random.Range(-range.y, range.y),
            Random.Range(-range.z, range.z)
        );
        Vector3 randomCoordinate = origin + randomRange;

        return randomCoordinate;
    }
}