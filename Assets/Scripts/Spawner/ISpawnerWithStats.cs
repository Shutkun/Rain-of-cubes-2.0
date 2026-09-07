using System;

public interface ISpawnerWithStats
{
    public event Action<int, int, int> Spawned;
}
