using CodeBase.Services.Input;
using CodeBase.Tools.Extension;
using UnityEngine;

namespace CodeBase.Logic.Movement
{
    public class HeroMover : Mover
    {
        private IInput _input;

        public void Construct(IInput input) =>
            _input = input;

        protected override Vector3 GetMoveDirection() =>
            _input.MoveDirection.AddY(0);

        protected override Quaternion GetLookRotation() =>
            Quaternion.LookRotation(Rigidbody.velocity.ChangeY(0));
    }
}