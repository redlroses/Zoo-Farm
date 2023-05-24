namespace CodeBase.Logic.Items
{
    public class Money : IItem
    {
        public ItemType Type { get; }
        public int Amount { get; }

        public Money(int amount)
        {
            Type = ItemType.Money;
            Amount = amount;
        }
    }
}