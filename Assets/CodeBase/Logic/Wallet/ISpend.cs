using System;

namespace CodeBase.Logic.Wallet
{
    public interface ISpend
    {
        bool TrySpend(Type item, int amount);
    }
}