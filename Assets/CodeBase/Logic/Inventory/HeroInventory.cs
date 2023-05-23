using UnityEngine;

namespace CodeBase.Logic.Inventory
{
    public class HeroInventory : MonoBehaviour
    {
        public IInventory Inventory { get; private set; }
    }
}