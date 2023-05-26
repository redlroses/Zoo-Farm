using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Items;
using CodeBase.Logic.Player;
using CodeBase.Logic.Wallet;
using NaughtyAttributes;
using UnityEngine;

namespace CodeBase.Logic.Interactions
{
    public class InteractionWalletPaidZone : InteractionPaidZone<HeroWallet>
    {
        [Dropdown("CurrencyNames")] [SerializeField]
        private string _currencyName;

        private List<Type> _currencyTypes;
        private Type _currencyType;

        private String[] CurrencyNames => _currencyTypes.Select(type => type.Name).ToArray();

        protected override void Start()
        {
            base.Start();
            FindTypes();
            SetChosenType();
        }

        private void OnValidate()
        {
            FindTypes();
            SetChosenType();
        }

        protected override void SetSpendable(ref ISpend spendable, HeroWallet target) =>
            spendable = target.Wallet;

        protected override bool TrySpend(ISpend spendable, int amount) =>
            spendable.TrySpend(_currencyType, amount);

        private void SetChosenType() =>
            _currencyType = _currencyTypes.Find(type => string.Equals(type.Name, _currencyName));

        private void FindTypes() => _currencyTypes = typeof(IItem).Assembly.ExportedTypes
            .Where(t => typeof(IItem).IsAssignableFrom(t) && t.IsInterface == false).ToList();
    }
}