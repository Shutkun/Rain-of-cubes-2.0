using System;

public interface ISpawnerWithStats
{
    event Action<int, int, int> Spawned;
}
