using CodeBase.Logic.Inventory;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class HeroInventory : MonoBehaviour
    {
        public IInventory Inventory { get; private set; }
    }
}