using CodeBase.Logic.Items;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Сollectible
{
    public class MoneyPack : MonoBehaviour, ICollectible
    {
        [SerializeField] private int _amountMoneyInPack;

        public IItem Item { get; private set; }
        public int Count { get; private set; }

        private void Awake()
        {
            Count = _amountMoneyInPack;
            Item = new Coin();
        }

        public void Collect() =>
            gameObject.Disable();
    }
}