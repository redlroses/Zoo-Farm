using CodeBase.Logic.Observer;
using CodeBase.Logic.Player;
using UnityEngine;

namespace CodeBase.Logic.Сollectible
{
    [RequireComponent(typeof(HeroInventory))]
    public class ItemCollector : ObserverTarget<ICollectible, TriggerObserver>
    {
        [SerializeField] private HeroInventory _heroInventory;
        [SerializeField] private HeroWallet _heroWallet;

        protected override void OnTargetEntered(ICollectible collectible)
        {
            if (collectible is MoneyPack moneyPack)
            {
                _heroWallet.Wallet.Replanish(moneyPack.AmountMoney);
                collectible.Collect();
                return;
            }

            if (_heroInventory.Inventory.TruAdd(collectible.Item))
            {
                collectible.Collect();
            }
        }
    }
}