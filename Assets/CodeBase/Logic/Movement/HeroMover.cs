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

        protected override Vector3 GetMoveDirection()
        {
            var cameraRotation = Camera.main.transform.parent.transform;
            var compensatedQuaternion = cameraRotation.TransformVector(_input.MoveDirection.AddY(0));
            return compensatedQuaternion;
        }

        protected override Quaternion GetLookRotation() =>
            Quaternion.LookRotation(Rigidbody.velocity.ChangeY(0));
    }
}