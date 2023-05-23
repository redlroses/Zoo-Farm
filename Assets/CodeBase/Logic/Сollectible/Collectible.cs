using CodeBase.Logic.Items;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Сollectible
{
    public class Collectible : MonoBehaviour, ICollectible
    {
        public IItem Item { get; private set; }

        public void Construct(int id, IItem item) =>
            Item = item;

        public void Disable() =>
            gameObject.Disable();
    }
}