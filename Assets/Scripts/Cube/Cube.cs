using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _minTimerValue = 2;
    [SerializeField] private int _maxTimerValue = 5;
    [Space]
    [SerializeField] private ColorController _colorRandom;
    [SerializeField] private TimerController _timerController;


    private bool _isColorChange = false;
    private Color _currentColor;
    private Vector3 _currentVelocity;
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Quaternion _currentRotation;

    public event Action<Cube> TimeOut;

    private void Awake()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _currentRotation = transform.rotation;
        _currentVelocity = _rigidbody.linearVelocity;
        _currentColor = _renderer.material.color;
    }

    private void OnEnable()
    {
        _timerController.TimerEnded += ResetParametrs;
    }

    private void OnDisable()
    {
        _timerController.TimerEnded -= ResetParametrs;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Platform platform))
        {
            int time = Random.Range(_minTimerValue, _maxTimerValue);
            _timerController.StartTimer(time);

            if (_isColorChange == false)
            {
                _colorRandom.ChangeColor(_renderer);
                _isColorChange = true;
            }
        }
    }

    private void ResetParametrs()
    {
        TimeOut?.Invoke(this);
        _isColorChange = false;
        _renderer.material.color = _currentColor;
        _rigidbody.linearVelocity = _currentVelocity;
        transform.rotation = _currentRotation;
        _timerController.StopTimer();
    }
}
