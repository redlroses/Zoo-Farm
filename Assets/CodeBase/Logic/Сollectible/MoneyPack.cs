using CodeBase.Logic.Items;
using UnityEngine;

namespace CodeBase.Logic.Сollectible
{
    public class MoneyPack : Collectible
    {
        [SerializeField] private int _amountMoneyInPack;
        public int AmountMoney => ((Money) Item).Amount;

        private void Awake() =>
            Construct(new Money(_amountMoneyInPack));
    }
}