using System;
using CodeBase.Logic;
using CodeBase.Logic.Interactions;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    [RequireComponent(typeof(TimerOperator))]
    public class SelectionZoneView : MonoBehaviour
    {
        [SerializeField] private TextSetterAnimated _textSetter;
        [SerializeField] private InteractionInventoryPaidZone _paidZone;
        [SerializeField] private float _disableDelay = 0.25f;

        private TimerOperator _timer;

        private void Awake()
        {
            _timer ??= GetComponent<TimerOperator>();
            _timer.SetUp(_disableDelay, () => gameObject.Disable());
        }

        private void OnEnable()
        {
            _textSetter.SetText(_paidZone.InteractionCost);
            _paidZone.CostChanged += OnCostChanged;
            _paidZone.FullPaid += OnFullPaid;
        }

        private void OnFullPaid() =>
            _timer.Restart();

        private void OnDisable()
        {
            _paidZone.CostChanged -= OnCostChanged;
            _paidZone.FullPaid -= OnFullPaid;
        }

        private void OnCostChanged(int cost) =>
            _textSetter.SetTextAnimated(cost);
    }
}