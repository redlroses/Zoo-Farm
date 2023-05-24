using System;
using System.Collections.Generic;
using CodeBase.Logic.Items;

namespace CodeBase.Logic.Inventory
{
    public interface IInventory : IEnumerable<IReadOnlyInventoryCell>
    {
        event Action<IReadOnlyInventoryCell> Updated;
        int Count { get; }
        bool TryUse(IItem storableType);
        void Cleanup();
        void Add(IItem collectible);
    }
}