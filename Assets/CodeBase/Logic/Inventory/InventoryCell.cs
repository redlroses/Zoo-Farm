using CodeBase.Logic.Items;

namespace CodeBase.Logic.Inventory
{
    public sealed class InventoryCell : IReadOnlyInventoryCell
    {
        public int Count { get; private set; }
        public IItem Item { get; }

        public bool IsEmpty => Count <= 0;

        public InventoryCell(IItem item)
        {
            Count = 0;
            Item = item;
        }

        public void IncreaseCount() =>
            Count++;

        public void DecreaseCount() =>
            Count--;
    }
}