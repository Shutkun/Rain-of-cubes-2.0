using System;

public interface ITimeoutable<T>
{
    public event Action<T> TimeOut;
}
