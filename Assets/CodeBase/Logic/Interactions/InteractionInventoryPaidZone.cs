using System;
using CodeBase.Logic.Inventory;
using CodeBase.Logic.Player;

namespace CodeBase.Logic.Interactions
{
    public class InteractionInventoryPaidZone : InteractionPaidZone<HeroInventory>
    {
        protected override void SetSpendable(ref ISpend spendable, HeroInventory target) =>
            spendable = target.Inventory;

        protected override bool TrySpend(ISpend spendable, Type currencyType, int amount) =>
            spendable.TrySpend(currencyType, amount);
    }
}