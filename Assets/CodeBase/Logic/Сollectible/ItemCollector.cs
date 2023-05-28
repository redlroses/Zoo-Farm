using CodeBase.Logic.Observer;

namespace CodeBase.Logic.Сollectible
{
    public class ItemCollector : ObserverTarget<ICollectibleItem, TriggerObserver>
    {
        protected override void OnTargetEntered(ICollectibleItem collectibleItem) =>
            collectibleItem.Collect();
    }
}