using CodeBase.Logic.Observer;
using CodeBase.Logic.Rabbit;
using UnityEngine;

namespace CodeBase.Logic.Сollectible
{
    public class RabbitItemCollector : ObserverTarget<ICollectible, TriggerObserver>
    {
        [SerializeField] private Satiety _satiety;

        protected override void OnTargetEntered(ICollectible collectibleItem)
        {
            Debug.Log("enter");
            _satiety.Replenish(1);
        }
    }
}