using System;

namespace CodeBase.Logic.Wallet
{
    public class Wallet : IWallet
    {
        private int _cashe;

        public event Action<int> Changed = c => { };

        public int Cashe
        {
            get => _cashe;
            set
            {
                _cashe = value;
                Changed.Invoke(_cashe);
            }
        }

        public void Replanish(int amount) =>
            Cashe += amount;

        public bool TrySpend(int amount)
        {
            if (Cashe - amount < 0)
                return false;

            Cashe -= amount;
            return true;
        }
    }
}