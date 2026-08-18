using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private int _poolMaxSize;

    public event Action<int,int> CubsSpawned;
    private float _timeOfWaiting = 0.7f;
    private int _objCount = 0;
    private int _totalSpawnObject = 0;
    private Coroutine _coroutine;
    private Spawners<Cube> _spawner;

    private void Awake()
    {
        _spawner = new Spawners<Cube>(_prefab, _poolMaxSize);
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
        _objCount--;
        CubsSpawned?.Invoke(_objCount,_totalSpawnObject);
        _spawner.Release(cube);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 origin = _startPoint.transform.position;
        Vector3 range = _startPoint.transform.localScale / 2f;
        Vector3 randomRange = new Vector3(
            Random.Range(-range.x, range.x),
            Random.Range(-range.y, range.y),
            Random.Range(-range.z, range.z)
        );
        Vector3 randomCoordinate = origin + randomRange;

        return randomCoordinate;
    }
}