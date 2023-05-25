using CodeBase.Logic.Interactions;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class SelectionZoneView : MonoBehaviour
    {
        [SerializeField] private GameObject _arrow;
        [SerializeField] private TextSetterAnimated _textSetter;
        [SerializeField] private InteractionWalletPaidZone _paidZone;

        private void OnEnable()
        {
            _textSetter.SetText(_paidZone.InteractionCost);
            _paidZone.CostChanged += PaidZoneOnCostChanged;
        }

        private void OnDisable() =>
            _paidZone.CostChanged -= PaidZoneOnCostChanged;

        private void PaidZoneOnCostChanged(int cost) =>
            _textSetter.SetTextAnimated(cost);
    }
}