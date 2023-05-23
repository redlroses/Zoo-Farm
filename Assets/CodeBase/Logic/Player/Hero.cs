using CodeBase.Logic.Movement;
using CodeBase.Services.Input;
using NTC.Global.Cache;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class Hero : MonoCache
    {
        [SerializeField] private HeroMover _mover;
        [SerializeField] private HeroAnimator _heroAnimator;

        private IPlayerInputService _inputService;

        private void OnDestroy()
        {
            _inputService.StartMove -= OnStartMove;
            _inputService.EndMove -= OnEndMove;
        }

        private void OnStartMove()
        {
            _mover.enabled = true;
            _heroAnimator.SetRun(true);
        }

        private void OnEndMove()
        {
            _mover.enabled = false;
            _heroAnimator.SetRun(false);
        }

        public void Construct(IPlayerInputService inputService)
        {
            _inputService = inputService;
            _inputService.StartMove += OnStartMove;
            _inputService.EndMove += OnEndMove;
        }
    }
}