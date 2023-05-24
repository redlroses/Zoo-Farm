namespace CodeBase.Logic.Items
{
    public class Money : IItem
    {
        public int Amount { get; }

        public Money(int amount) =>
            Amount = amount;
    }
}