using System;
using System.Collections.Generic;
using System.Diagnostics;
using CodeBase.Logic.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

namespace CodeBase.Logic.Player
{
    public class HeroInventory : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private int _maxWeight = 4;

        public IInventory Inventory { get; private set; }

        private void OnDestroy()
        {
            Inventory.Replenished -= OnUpdatedInventory;
            Inventory.Spend -= OnUpdatedInventory;
        }

        public void Construct()
        {
            Inventory = new Inventory.Inventory(new List<IReadOnlyInventoryCell>(), _maxWeight);
            Inventory.Replenished += OnUpdatedInventory;
            Inventory.Spend += OnUpdatedInventory;
        }

        private void OnUpdatedInventory(IReadOnlyInventoryCell _) =>
            _heroAnimator.SetHold(Inventory.Weight > 0);

        [Conditional("UNITY_EDITOR")]
        private void Start()
        {
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