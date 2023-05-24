using System;

namespace CodeBase.Logic.Сollectible
{
    public class CarrotFruit : Collectible, ICollectibleReactive
    {
        public event Action Collected = () => { };

        protected override void OnCollected() =>
            Collected.Invoke();
    }
}