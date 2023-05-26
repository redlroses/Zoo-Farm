using System;
using CodeBase.Logic.Items;

namespace CodeBase.Logic.Wallet
{
    public interface ISpend
    {
        bool TrySpend(Type item, int amount);
    }
}