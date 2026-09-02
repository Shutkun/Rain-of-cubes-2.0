using System;

public interface ITimeoutable<T>
{
    event Action<T> TimeOut;
}
