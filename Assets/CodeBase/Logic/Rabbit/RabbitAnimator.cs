using UnityEngine;

namespace CodeBase.Logic.Rabbit
{
    public class RabbitAnimator : MonoBehaviour
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Run = Animator.StringToHash("Run");

        [SerializeField] private Animator _animator;

        public void SetIdle() =>
            _animator.CrossFade(Idle, 0.1f);

        public void SetMove() =>
            _animator.CrossFade(Run, 0.1f);
    }
}