using System;
using System.Collections.Generic;
using CodeBase.Logic.Items;
using CodeBase.Logic.Wallet;

namespace CodeBase.Logic.Inventory
{
    public interface IInventory : IEnumerable<IReadOnlyInventoryCell>, ISpend
    {
        event Action<IReadOnlyInventoryCell> Updated;
        int Count { get; }
        void Cleanup();
        bool TruAdd(IItem collectible);
        bool IsFull { get; }
    }
}