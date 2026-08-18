using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private int _minTimerValue = 2;
    [SerializeField] private int _maxTimerValue = 5;
    [Space]
    [SerializeField] private ColorController _colorRandom;
    [SerializeField] private TimerController _timerController;

    public event Action<Bomb> TimeOut;
    private Renderer _renderer;
    private Color _currentColor;


    private void Awake()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _currentColor = _renderer.material.color;
    }
    private void OnEnable()
    {
        _timerController.TimerEnded += ResetParametrs;
        ChangeFade();
    }

    private void OnDisable()
    {
        _timerController.TimerEnded -= ResetParametrs;
    }

    public void SetPosition(Vector3 position) =>
        gameObject.transform.position = position;


    private void ChangeFade()
    {
        int time = Random.Range(_minTimerValue, _maxTimerValue);
        _timerController.StartTimer(time);
        _colorRandom.StratChangeAlha(_renderer,time);
    }

    private void ResetParametrs()
    {
        _renderer.material.color = _currentColor;
        _timerController.StopTimer();
        TimeOut?.Invoke(this);
    }
}
