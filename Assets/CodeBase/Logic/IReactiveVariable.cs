using System;

namespace CodeBase.Logic
{
    public interface IReactiveVariable<out T>
    {
        event Action<T> Changed;
    }
}