using CodeBase.Logic.Items;

namespace CodeBase.Logic.Wallet
{
    public interface ISpend
    {
        bool TrySpend(ItemType item, int amount);
    }
}