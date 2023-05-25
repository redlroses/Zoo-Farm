﻿using CodeBase.Logic.Items;
using CodeBase.Logic.Player;
using CodeBase.Logic.Wallet;

namespace CodeBase.Logic.Interactions
{
    public class InteractionInventoryPaidZone : InteractionPaidZone<HeroInventory>
    {
        protected override void SetSpendable(ref ISpend spendable, HeroInventory target) =>
            spendable = target.Inventory;

        protected override bool TrySpend(ISpend spendable, int amount) =>
            spendable.TrySpend(ItemType.Carrot, amount);
    }
}