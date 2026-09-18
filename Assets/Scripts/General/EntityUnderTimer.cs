using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class EntityUnderTimer <T>: MonoBehaviour, ITimeoutable<T>
    where T : EntityUnderTimer <T>
{
    [SerializeField] protected int MinTimerValue = 2;
    [SerializeField] protected int MaxTimerValue = 5;
    [Space]
    [SerializeField] protected ColorController ColorController;
    [SerializeField] protected TimerController TimerController;

    protected Renderer Renderer;
    private Color _currentColor;
    public event Action<T> TimeOut;

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        _currentColor = Renderer.material.color;
        AwakeSetup();
    }

    private void OnEnable()
    {
        TimerController.TimerEnded += OnTimerEnded;
        StartTimer();
    }


    private void OnDisable()
    {
        TimerController.TimerEnded -= OnTimerEnded;
    }

    protected void StartTimer()
    {
        int time = Random.Range(MinTimerValue, MaxTimerValue + 1);
        TimerController.Star(time);
        StartedTimer(time);
    }

    protected virtual void ResetState()
    {
        TimerController.Stop();
        Renderer.material.color = _currentColor;

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
