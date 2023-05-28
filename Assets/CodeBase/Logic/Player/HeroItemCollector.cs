using CodeBase.Logic.Observer;
using CodeBase.Logic.Сollectible;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    [RequireComponent(typeof(HeroInventory))]
    public class HeroItemCollector : ObserverTarget<ICollectibleItem, TriggerObserver>
    {
        [SerializeField] private HeroInventory _heroInventory;

        protected override void OnTargetEntered(ICollectibleItem collectibleItem)
        {
            if (_heroInventory.Inventory.TryAdd(collectibleItem.Item, collectibleItem.Count))
            {
                collectibleItem.Collect();
            }
        }
    }
}