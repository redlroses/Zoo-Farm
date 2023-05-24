using CodeBase.Logic.Inventory;
using CodeBase.Logic.Observer;
using UnityEngine;

namespace CodeBase.Logic.Сollectible
{
    [RequireComponent(typeof(HeroInventory))]
    public class ItemCollector : ObserverTarget<ICollectible, TriggerObserver>
    {
        [SerializeField] private HeroInventory _heroInventory;

        protected override void OnTargetEntered(ICollectible collectible)
        {
            // _heroInventory.Inventory.Add(collectible.Item);
            collectible.Disable();
            Debug.Log("collectible picked");
        }
    }
}