using CodeBase.Logic;
using CodeBase.Logic.Rabbit;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    [RequireComponent(typeof(TimerOperator))]
    public class SatietyView : MonoBehaviour
    {
        [SerializeField] private float _disableDelay;
        [SerializeField] private Satiety _satiety;
        [SerializeField] private TextSetterAnimated _textSetter;

        private TimerOperator _timer;
        private Canvas _canvas;

        private void Awake()
        {
            _timer ??= GetComponent<TimerOperator>();
            _canvas ??= GetComponent<Canvas>();
            _timer.SetUp(_disableDelay, () => _canvas.enabled = false);
        }

        private void OnEnable() =>
            _satiety.Updated += OnSatietyUpdated;

        private void OnDisable() =>
            _satiety.Updated -= OnSatietyUpdated;

        private void OnSatietyUpdated()
        {
            if (_satiety.CurrentLevel == _satiety.MaxLevel)
            {
                _timer.Restart();
            }
            else
            {
                _canvas.enabled = true;
            }

            _textSetter.SetTextAnimated(_satiety.CurrentLevel, $"/{_satiety.MaxLevel}");
        }
    }
}