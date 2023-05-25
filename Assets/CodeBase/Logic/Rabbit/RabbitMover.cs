using CodeBase.Logic.Movement;
using CodeBase.Tools.Extension;
using UnityEngine;

namespace CodeBase.Logic.Rabbit
{
    public class RabbitMover : Mover
    {
        private Vector3 _destinationPoint;

        public Vector3 DestinationPoint => _destinationPoint;

        public void SetDestination(Vector3 position) =>
            _destinationPoint = position.ChangeY(Rigidbody.position.y);

        protected override Vector3 GetMoveDirection() =>
            (DestinationPoint - Rigidbody.position).normalized;

        protected override Quaternion GetLookRotation() =>
            Quaternion.LookRotation(Rigidbody.velocity.ChangeY(0));
    }
}