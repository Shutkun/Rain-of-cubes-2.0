using System;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    public event Action<Vector3> BombReleased;

    private void OnEnable()
    {
        _cubeSpawner.CubRealeased += Spawn;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubRealeased -= Spawn;
    }

    protected override void OnObjectReleased(Bomb bomb)
    {
        BombReleased?.Invoke(bomb.transform.position);
    }

    private void Spawn(Transform cubeTransform)
    {
        SpawnObject(cubeTransform.position);
    }
}
