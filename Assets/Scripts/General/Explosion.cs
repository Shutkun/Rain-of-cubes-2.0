using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _force = 5;
    [Space]
    [SerializeField] private BombSpawner _bombSpawner;


    private void OnEnable()
    {
        _bombSpawner.BombReleased += Explode;
    }

    private void OnDisable()
    {
        _bombSpawner.BombReleased -= Explode;
    }

    private void Explode(Vector3 bombPosition)
    {
        Collider[] overlappColliders = Physics.OverlapSphere(bombPosition, _radius);

        foreach (Collider col in overlappColliders)
        {
            if (col.TryGetComponent<Rigidbody>(out Rigidbody component))
            {
                component.AddExplosionForce(_force, bombPosition, _radius);
            }
        }
    }
}
