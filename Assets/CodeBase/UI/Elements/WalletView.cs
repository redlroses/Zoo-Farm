using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TextSetterAnimated _textSetter;

        private IReactiveVariable<int> _moneyAmount;

        private void OnDestroy() =>
            _moneyAmount.Changed -= MoneyAmountOnChanged;

        public void Construct(IReactiveVariable<int> moneyAmount)
        {
            _moneyAmount = moneyAmount;
            _moneyAmount.Changed += MoneyAmountOnChanged;
        }

        private void MoneyAmountOnChanged(int amount) =>
            _textSetter.SetTextAnimated(amount);
    }
}