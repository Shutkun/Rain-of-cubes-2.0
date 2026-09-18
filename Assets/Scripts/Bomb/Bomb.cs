using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bomb : EntityUnderTimer<Bomb>
{
    protected override void StartedTimer(int time) 
    {
        ColorController.StartChangeAlpha(Renderer, time);
    }
}
