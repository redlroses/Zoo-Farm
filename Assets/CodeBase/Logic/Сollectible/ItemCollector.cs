using CodeBase.Logic.Observer;

namespace CodeBase.Logic.Сollectible
{
    public class ItemCollector : ObserverTarget<ICollectible, TriggerObserver>
    {
        protected override void OnTargetEntered(ICollectible collectible) =>
            collectible.Collect();
    }
}