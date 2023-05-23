using CodeBase.StaticData.Storable;
using UnityEngine;

namespace CodeBase.Logic.Items
{
    public interface IItem
    {
        StorableType ItemType { get; }
    }
}