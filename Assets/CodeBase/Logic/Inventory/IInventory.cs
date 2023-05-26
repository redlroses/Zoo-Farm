using System;
using System.Collections.Generic;
using CodeBase.Logic.Items;
using CodeBase.Logic.Wallet;

namespace CodeBase.Logic.Inventory
{
    public interface IInventory : IEnumerable<IReadOnlyInventoryCell>, ISpend
    {
        event Action<IReadOnlyInventoryCell> Updated;
        int Weight { get; }
        void Cleanup();
        bool TryAdd(IItem collectible, int amount = 1);
        bool IsFull { get; }
    }
}