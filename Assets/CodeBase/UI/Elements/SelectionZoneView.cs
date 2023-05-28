using CodeBase.Logic.Interactions;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class SelectionZoneView : MonoBehaviour
    {
        [SerializeField] private GameObject _arrow;
        [SerializeField] private TextSetterAnimated _textSetter;
        [SerializeField] private InteractionInventoryPaidZone _paidZone;

        private void OnEnable()
        {
            _textSetter.SetText(_paidZone.InteractionCost);
            _paidZone.CostChanged += OnCostChanged;
            _paidZone.FullPaid += OnFullPaid;
        }

        private void OnFullPaid() =>
            gameObject.Disable();

        private void OnDisable()
        {
            _paidZone.CostChanged -= OnCostChanged;
            _paidZone.FullPaid -= OnFullPaid;
        }

        private void OnCostChanged(int cost) =>
            _textSetter.SetTextAnimated(cost);
    }
}