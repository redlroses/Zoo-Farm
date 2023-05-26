using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Items;
using UnityEngine;

namespace CodeBase.Logic.Inventory
{
    public sealed class Inventory : IReadOnlyCollection<IReadOnlyInventoryCell>, IInventory
    {
        private readonly List<InventoryCell> _storage;
        private readonly int _maxWeight;

        private int _itemsWeight;

        public event Action<IReadOnlyInventoryCell> Updated = c => { };

        public int Weight => _itemsWeight;
        public bool IsFull => _itemsWeight >= _maxWeight;

        public int Count => _storage.Count;

        public Inventory(List<IReadOnlyInventoryCell> storage, int maxWeight)
        {
            _maxWeight = maxWeight;
            _storage = storage.ConvertAll(cell => (InventoryCell) cell);
        }

        public IEnumerator<IReadOnlyInventoryCell> GetEnumerator() => _storage.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Cleanup() =>
            _storage.Clear();

        public bool TryAdd(IItem item, int amount = 1)
        {
            if (IsFull)
                return false;

            if (IsAllowableWeight(item, amount) == false)
                return false;

            if (TryGetInventoryCell(item.GetType(), out InventoryCell inventoryCell))
            {
                inventoryCell.IncreaseCount();
            }
            else
            {
                inventoryCell = new InventoryCell(item);
                _storage.Add(inventoryCell);
            }

            Updated.Invoke(inventoryCell);
            _itemsWeight++;
            return true;
        }

        public bool TrySpend(Type itemType, int amount)
        {
            Debug.Log(itemType);

            if (TryGetInventoryCell(itemType, out InventoryCell inventoryCell) == false)
                return false;

            if (inventoryCell.Count <= amount)
                return false;

            for (int i = 0; i < amount; i++)
                Spend(inventoryCell);

            return true;
        }

        private void Spend(InventoryCell inventoryCell)
        {
            inventoryCell.DecreaseCount();
            _itemsWeight--;

            if (inventoryCell.IsEmpty)
                _storage.Remove(inventoryCell);

            Updated.Invoke(inventoryCell);
        }

        private bool IsAllowableWeight(IItem item, int amount)
        {
            if (item is IWeighty weighty)
            {
                int totalWeight = weighty.Weight * amount;
                return totalWeight + Weight <= _maxWeight;
            }

            return true;
        }

        private bool TryGetInventoryCell(Type byType, out InventoryCell inventoryCell)
        {
            inventoryCell = GetExistingInventoryCell(byType);
            return inventoryCell != null;
        }

        private InventoryCell GetExistingInventoryCell(Type byType)
        {
            InventoryCell existingInventoryCell =
                _storage.FirstOrDefault(inventoryCell => inventoryCell.Item.GetType() == byType);
            return existingInventoryCell;
        }
    }
}