using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    private Renderer _renderer;

    private void Start()
    {
        _renderer = gameObject.GetComponent<Renderer>();
    }

    public void SetPosition(Vector3 position) =>
        gameObject.transform.position = position;

    public void SetAlpha(float alpha)
    {
        Color currentColor = _renderer.material.color;
        _renderer.material.SetColor(" ", new Color(currentColor.r, currentColor.g, currentColor.b, alpha));
    }
}
