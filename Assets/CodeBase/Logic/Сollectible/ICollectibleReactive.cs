using System;

namespace CodeBase.Logic.Сollectible
{
    public interface ICollectibleReactive : ICollectible
    {
        event Action Collected;
    }
}