using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Items;

namespace CodeBase.Logic.Inventory
{
    public sealed class Inventory : IReadOnlyCollection<IReadOnlyInventoryCell>, IInventory
    {
        private readonly List<InventoryCell> _storage;
        private readonly int _maxItems;

        private int _itemsCount;

        public event Action<IReadOnlyInventoryCell> Updated = c => { };

        public int Count => _itemsCount;

        public bool IsFull => _itemsCount >= _maxItems;

        public Inventory(List<IReadOnlyInventoryCell> storage, int maxItems)
        {
            _maxItems = maxItems;
            _storage = storage.ConvertAll(cell => (InventoryCell) cell);
        }

        public IEnumerator<IReadOnlyInventoryCell> GetEnumerator() => _storage.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Cleanup() =>
            _storage.Clear();

        public bool TruAdd(IItem item)
        {
            if (IsFull)
                return false;

            if (TryGetInventoryCell(item.Type, out InventoryCell inventoryCell))
            {
                inventoryCell.Increase();
            }
            else
            {
                inventoryCell = new InventoryCell(item);
                _storage.Add(inventoryCell);
            }

            Updated.Invoke(inventoryCell);
            _itemsCount++;
            return true;
        }

        public bool TryUse(IItem itemType)
        {
            if (TryGetInventoryCell(itemType.Type, out InventoryCell inventoryCell))
            {
                Expend(inventoryCell);
                return true;
            }

            return false;
        }

        private void Expend(InventoryCell existingInventoryCell)
        {
            existingInventoryCell.Decrease();
            _itemsCount--;

            if (existingInventoryCell.IsEmpty)
                _storage.Remove(existingInventoryCell);

            Updated.Invoke(existingInventoryCell);
        }

        private bool TryGetInventoryCell(ItemType byType, out InventoryCell inventoryCell)
        {
            inventoryCell = GetExistingInventoryCell(byType);
            return inventoryCell != null;
        }

        private InventoryCell GetExistingInventoryCell(ItemType storableType)
        {
            InventoryCell existingInventoryCell =
                _storage.FirstOrDefault(inventoryCell => inventoryCell.Item.Type == storableType);
            return existingInventoryCell;
        }
    }
}