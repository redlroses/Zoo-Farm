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
                print(_heroWallet.Wallet);
                _heroWallet.Wallet.Replanish(moneyPack.AmountMoney);
            }

            // _heroInventory.Inventory.Add(collectible.Item);
            collectible.Disable();
            Debug.Log("collectible picked");
        }
    }
}