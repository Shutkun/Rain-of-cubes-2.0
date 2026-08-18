using System;
using System.Collections;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public event Action TimerEnded;
    private Coroutine _coroutine;

    public void StartTimer(int time)
    {
        StopTimer();
        _coroutine = StartCoroutine(disappearingTimer(time));
    }

    public void StopTimer()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator disappearingTimer(int time)
    {
        WaitForSeconds _waitForSeconds = new WaitForSeconds(time);

        yield return _waitForSeconds;

        TimerEnded?.Invoke();
    }
}
