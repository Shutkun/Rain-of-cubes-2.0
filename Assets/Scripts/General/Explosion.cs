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

    private void Explode(Bomb bomb)
    {
        Collider[] overlappColliders = Physics.OverlapSphere(bomb.transform.position, _radius);

        foreach (Collider col in overlappColliders)
        {
            if (col.TryGetComponent<Rigidbody>(out Rigidbody component))
            {
                component.AddExplosionForce(_force, bomb.transform.position, _radius);
            }
        }
    }
}
