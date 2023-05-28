using CodeBase.Logic.Inventory;
using CodeBase.Logic.Items;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TextSetterAnimated _textSetter;

        private IInventory _inventory;

        private void OnDestroy()
        {
            _inventory.Replenished -= OnInventoryReplenished;
            _inventory.Spend -= OnInventoryReplenished;
        }

        public void Construct(IInventory inventory)
        {
            _inventory = inventory;
            _inventory.Replenished += OnInventoryReplenished;
            _inventory.Spend += OnInventoryReplenished;
        }

        private void OnInventoryReplenished(IReadOnlyInventoryCell cell)
        {
            if (cell.Item is Coin)
            {
                _textSetter.SetTextAnimated(cell.Count);
            }
        }
    }
}