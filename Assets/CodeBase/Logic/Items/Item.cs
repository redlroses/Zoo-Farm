using CodeBase.StaticData.Storable;
using UnityEngine;

namespace CodeBase.Logic.Items
{
    public class Item : IItem
    {
        public StorableType ItemType { get; }

        public Item(StorableType itemType) =>
            ItemType = itemType;
    }
}