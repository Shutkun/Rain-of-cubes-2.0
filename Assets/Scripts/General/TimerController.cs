using System;
using System.Collections;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private Coroutine _coroutine;
    public event Action TimerEnded;

    public void Star(int time)
    {
        Stop();
        _coroutine = StartCoroutine(Disappearing(time));
    }

    public void Stop()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator Disappearing(int time)
    {
        WaitForSeconds wait = new WaitForSeconds(time);

        yield return wait;

        TimerEnded?.Invoke();
    }
}
