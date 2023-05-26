using System;
using CodeBase.Logic.Items;

namespace CodeBase.Logic.Wallet
{
    public class Wallet : IWallet
    {
        private int _cashe;

        public event Action<int> Changed = c => { };

        public int Cashe
        {
            get => _cashe;
            private set
            {
                _cashe = value;
                Changed.Invoke(_cashe);
            }
        }

        public void Replanish(int amount) =>
            Cashe += amount;

        public bool TrySpend(Type item, int amount)
        {
            if (item != typeof(Coin))
            {
                return false;
            }

            if (Cashe - amount < 0)
                return false;

            Cashe -= amount;
            return true;
        }
    }
}