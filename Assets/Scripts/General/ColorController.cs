using System.Collections;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    private Coroutine _coroutine;

    private void OnDisable()
    {
        StopChangeAlpha();
    }

    public void ChangeColor(Renderer renderer) =>
        renderer.material.color = Random.ColorHSV();

    public void StratChangeAlha(Renderer renderer, float time)
    {
        StopChangeAlpha();

        _coroutine = StartCoroutine(SetAlpha(renderer, time));
    }

    public void StopChangeAlpha()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = null;
    }

    private IEnumerator SetAlpha(Renderer renderer, float time)
    {
        Color currentColor = renderer.material.color;
        float startAlpha = currentColor.a;
        float targetAlpha = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / time);

            renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);

            yield return null;
        }

        renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);
    }
}
