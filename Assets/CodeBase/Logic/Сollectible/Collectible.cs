using CodeBase.Logic.Items;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Сollectible
{
    public class Collectible : MonoBehaviour, ICollectible
    {
        public IItem Item { get; private set; }
        public int Count { get; private set; }

        public void Construct(IItem item, int count)
        {
            Item = item;
            Count = count;
        }

        public void Collect()
        {
            OnCollected();
            gameObject.Disable();
        }

        protected virtual void OnCollected() { }
    }
}