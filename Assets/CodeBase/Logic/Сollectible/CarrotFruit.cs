using System;
using CodeBase.Logic.Items;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Сollectible
{
    public class CarrotFruit : MonoBehaviour, ICollectibleItemReactive
    {
        [SerializeField] private int _weight;
        [SerializeField] private int _count;

        public event Action Collected = () => { };

        public IItem Item { get; private set; }
        public int Count { get; private set; }

        private void Awake()
        {
            Count = _count;
            Item = new Items.Carrot(_weight);
        }

        public void Collect()
        {
            gameObject.Disable();
            Collected.Invoke();
        }
    }
}