using CodeBase.Logic.Inventory;
using CodeBase.Logic.Items;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TextSetterAnimated _textSetter;

        private IInventory _inventory;

        private void OnDestroy() =>
            _inventory.Updated -= OnInventoryUpdated;

        public void Construct(IInventory inventory)
        {
            _inventory = inventory;
            _inventory.Updated += OnInventoryUpdated;
        }

        private void OnInventoryUpdated(IReadOnlyInventoryCell cell)
        {
            Debug.Log("OnInventoryUpdated");

            if (cell.Item is Coin)
            {
                Debug.Log("Coin");
                _textSetter.SetTextAnimated(cell.Count);
            }
        }
    }
}