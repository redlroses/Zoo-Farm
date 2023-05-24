namespace CodeBase.Logic.Wallet
{
    public interface IWallet : IReplanish, ISpend, IReactiveVariable<int>
    {
        int Cashe { get; }
    }
}