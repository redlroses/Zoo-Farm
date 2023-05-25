using CodeBase.Logic.Items;
using CodeBase.Logic.Player;
using CodeBase.Logic.Wallet;

namespace CodeBase.Logic.Interactions
{
    public class InteractionWalletPaidZone : InteractionPaidZone<HeroWallet>
    {
        protected override void SetSpendable(ref ISpend spendable, HeroWallet target) =>
            spendable = target.Wallet;

        protected override bool TrySpend(ISpend spendable, int amount) =>
            spendable.TrySpend(ItemType.Money, amount);
    }
}