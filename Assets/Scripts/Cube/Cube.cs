using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : EntityUnderTimer<Cube>
{
    private bool _isColorChange = false;
    private Vector3 _currentVelocity;
    private Rigidbody _rigidbody;
    private Quaternion _currentRotation;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Platform platform))
        {
            StartTimer();
            if (_isColorChange == false)
            {
                _colorController.ChangeColor(_renderer);
                _isColorChange = true;
            }
        }
    }

    protected override void AwakeSetup()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentRotation = transform.rotation;
        _currentVelocity = _rigidbody.linearVelocity;
    }

    protected override void ResetSpecific()
    {
        _isColorChange = false;
        _rigidbody.linearVelocity = _currentVelocity;
        transform.rotation = _currentRotation;
    }
}
