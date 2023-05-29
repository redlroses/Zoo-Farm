using System;
using System.Collections.Generic;
using CodeBase.Logic.Items;

namespace CodeBase.Logic.Inventory
{
    public interface IInventory : IEnumerable<IReadOnlyInventoryCell>, ISpend
    {
        event Action<IReadOnlyInventoryCell> Replenished;
        event Action<IReadOnlyInventoryCell> Spend;
        int Weight { get; }
        bool IsFull { get; }
        void Cleanup();
        bool TryAdd(IItem collectible, int amount = 1);
    }
}