using System;

namespace CodeBase.Logic.Inventory
{
    public interface ISpend
    {
        bool TrySpend(Type item, int amount);
    }
}