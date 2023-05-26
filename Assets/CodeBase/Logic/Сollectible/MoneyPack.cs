using CodeBase.Logic.Items;
using UnityEngine;

namespace CodeBase.Logic.Сollectible
{
    public class MoneyPack : Collectible
    {
        [SerializeField] private int _amountMoneyInPack;
        public int AmountMoney => _amountMoneyInPack;

        private void Awake() =>
            Construct(new Coin());
    }
}