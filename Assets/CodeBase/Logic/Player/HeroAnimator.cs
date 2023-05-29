using System;
using CodeBase.Logic.AnimatorStateMachine;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        private const string TopLayerName = "Top Layer";

        private static readonly int IsRun = Animator.StringToHash("IsRun");

        private readonly int _runStateHash = Animator.StringToHash("Run");
        private readonly int _idleStateHash = Animator.StringToHash("Idle");

        private int _topLayerIndex;

        [SerializeField] private Animator _animator;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }

        private void Awake() =>
            _topLayerIndex = _animator.GetLayerIndex(TopLayerName);

        public void SetHold(bool isHold) =>
            _animator.SetLayerWeight(_topLayerIndex, Convert.ToInt32(isHold));

        public void SetRun(bool isRun) =>
            _animator.SetBool(IsRun, isRun);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) =>
            StateExited?.Invoke(StateFor(stateHash));

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
            {
                state = AnimatorState.Idle;
            }
            else if (stateHash == _runStateHash)
            {
                state = AnimatorState.Run;
            }
            else
            {
                state = AnimatorState.Unknown;
            }

            return state;
        }
    }
}