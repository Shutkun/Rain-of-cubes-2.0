using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Transform _startPoint;

    public event Action<Transform> CubRealeased;
    private float _timeOfWaiting = 0.5f;
    private Coroutine _coroutine;


    private void Start()
    {
        _coroutine = StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    protected override void OnObjectReleased(Cube cube)
    {
        CubRealeased?.Invoke(cube.transform);
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeOfWaiting);

        while (enabled)
        {
            SpawnObject(GetRandomPosition());
            yield return wait;
        }
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