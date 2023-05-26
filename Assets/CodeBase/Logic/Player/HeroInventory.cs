using System.Collections.Generic;
using CodeBase.Logic.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Logic.Player
{
    public class HeroInventory : MonoBehaviour
    {
        [SerializeField] private int _maxWeight = 4;

        public IInventory Inventory { get; private set; }

        private void Start()
        {
            Inventory = new Inventory.Inventory(new List<IReadOnlyInventoryCell>(), _maxWeight);

            Debug.Log("Press \"E\" for look into inventory");

            InputAction input = new InputAction("Press E", InputActionType.Button, "<Keyboard>/E");
            input.started += context =>
            {
                foreach (IReadOnlyInventoryCell cell in Inventory)
                {
                    Debug.Log($"Item:  {cell.Item}, type: {cell.Item.GetType()}, count: {cell.Count}");
                }
            };

            input.Enable();
        }
    }
}