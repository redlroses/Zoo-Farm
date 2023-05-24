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

        public event Action<IReadOnlyInventoryCell> Updated = c => { };

        public int Count => _storage.Count;

        public Inventory(List<IReadOnlyInventoryCell> storage) =>
            _storage = storage.ConvertAll(cell => (InventoryCell) cell);

        public void Cleanup() =>
            _storage.Clear();

        public void Add(IItem item)
        {
            if (TryGetInventoryCell(item, out InventoryCell inventoryCell))
            {
                inventoryCell.Increase();
                Debug.Log($"Count item {inventoryCell.Count}");
            }
            else
            {
                inventoryCell = new InventoryCell(item);
                _storage.Add(inventoryCell);
            }

            Updated.Invoke(inventoryCell);
        }

        public bool TryUse(IItem storableType) =>
            TryGetInventoryCell(storableType, out InventoryCell _);

        private bool TryGetInventoryCell(IItem byType, out InventoryCell inventoryCell)
        {
            inventoryCell = GetExistingInventoryCell(byType);
            return inventoryCell != null;
        }

        private InventoryCell GetExistingInventoryCell(IItem item)
        {
            InventoryCell existingInventoryCell =
                _storage.FirstOrDefault(inventoryCell => inventoryCell.Item == item);
            return existingInventoryCell;
        }

        public IEnumerator<IReadOnlyInventoryCell> GetEnumerator() => _storage.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}