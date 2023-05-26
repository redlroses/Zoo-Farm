using System;
using System.Linq;
using CodeBase.Logic.Items;
using CodeBase.Logic.Player;
using CodeBase.Logic.Wallet;
using NaughtyAttributes;
using UnityEngine;

namespace CodeBase.Logic.Interactions
{
    public class InteractionInventoryPaidZone : InteractionPaidZone<HeroInventory>
    {
        [Dropdown("_currencyTypes")]
        [SerializeField] private Type _currency;

        private Type[] _currencyTypes;

        protected override void Start()
        {
            base.Start();
            FindTypes();
        }

        protected override void SetSpendable(ref ISpend spendable, HeroInventory target) =>
            spendable = target.Inventory;

        protected override bool TrySpend(ISpend spendable, int amount) =>
            spendable.TrySpend(_currency, amount);

        private void FindTypes()
        {
            var baseType = typeof(IItem);
            _currencyTypes = baseType.Assembly.ExportedTypes.Where(t => baseType.IsAssignableFrom(t) && t.IsInterface == false).ToArray();

            Debug.Log("types: ");

            foreach (var info in _currencyTypes)
            {
                Debug.Log(info.Name);
            }
        }
    }
}