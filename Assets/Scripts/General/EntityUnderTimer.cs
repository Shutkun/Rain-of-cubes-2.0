using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class EntityUnderTimer <T>: MonoBehaviour, ITimeoutable<T>
    where T : EntityUnderTimer <T>
{
    [SerializeField] protected int _minTimerValue = 2;
    [SerializeField] protected int _maxTimerValue = 5;
    [Space]
    [SerializeField] protected ColorController _colorController;
    [SerializeField] protected TimerController _timerController;

    protected Renderer _renderer;
    private Color _currentColor;
    public event Action<T> TimeOut;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _currentColor = _renderer.material.color;
        AwakeSetup();
    }

    private void OnEnable()
    {
        _timerController.TimerEnded += OnTimerEnded;
        StartTimer();
    }


    private void OnDisable()
    {
        _timerController.TimerEnded -= OnTimerEnded;
    }

    protected void StartTimer()
    {
        int time = Random.Range(_minTimerValue, _maxTimerValue + 1);
        _timerController.Star(time);
        StartedTimer(time);
    }

    protected virtual void ResetState()
    {
        _timerController.Stop();
        _renderer.material.color = _currentColor;

        ResetSpecific();
    }

    protected virtual void ResetSpecific() { }

    protected virtual void AwakeSetup() { }

    protected virtual void StartedTimer(int time) { }

    private void OnTimerEnded()
    {
        TimeOut?.Invoke((T)this);
        ResetState();
    }
}
