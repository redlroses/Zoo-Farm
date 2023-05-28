using CodeBase.Logic.Observer;
using CodeBase.Logic.Player;
using UnityEngine;

namespace CodeBase.Logic.Сollectible
{
    [RequireComponent(typeof(HeroInventory))]
    public class HeroItemCollector : ObserverTarget<ICollectible, TriggerObserver>
    {
        [SerializeField] private HeroInventory _heroInventory;

        protected override void OnTargetEntered(ICollectible collectible)
        {
            if (_heroInventory.Inventory.TryAdd(collectible.Item, collectible.Count))
            {
                collectible.Collect();
            }
        }
    }
}