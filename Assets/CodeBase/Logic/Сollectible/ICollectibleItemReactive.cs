using System;

namespace CodeBase.Logic.Сollectible
{
    public interface ICollectibleItemReactive : ICollectibleItem
    {
        event Action Collected;
    }
}