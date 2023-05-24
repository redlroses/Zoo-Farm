namespace CodeBase.Logic.Wallet
{
    public interface ISpend
    {
        bool TrySpend(int amount);
    }
}