using System;

public static class SpawnStatistics
{
    private static int _totalCreated = 0;
    public static event Action<int> TotalCreatedChanged;

    public static int TotalCreated => _totalCreated;

    public static void AddCreated()
    {
        _totalCreated++;
        TotalCreatedChanged?.Invoke(_totalCreated);
    }

}
