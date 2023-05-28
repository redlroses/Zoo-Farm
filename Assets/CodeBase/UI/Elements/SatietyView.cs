using CodeBase.Logic.Rabbit;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class SatietyView : MonoBehaviour
    {
        [SerializeField] private Satiety _satiety;
        [SerializeField] private TextSetterAnimated _textSetter;

        private void OnEnable() =>
            _satiety.Updated += OnSatietyUpdated;

        private void OnDisable() =>
            _satiety.Updated -= OnSatietyUpdated;

        private void OnSatietyUpdated() =>
            _textSetter.SetTextAnimated(_satiety.CurrentLevel, $"/{_satiety.MaxLevel}");
    }
}