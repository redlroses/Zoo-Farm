using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Services.Input
{
    public class PlayerInputService : IPlayerInputService
    {
        private readonly InputActions _inputActions;

        public event Action StartMove = () => { };
        public event Action EndMove = () => { };

        public Vector2 MoveDirection => _inputActions.Movment.Move.ReadValue<Vector2>();

        public PlayerInputService() =>
            _inputActions = new InputActions();

        public void Subscribe()
        {
            _inputActions.Movment.Enable();
            _inputActions.Movment.Move.started += OnStartMove;
            _inputActions.Movment.Move.canceled += OnEndMove;
        }

        public void Cleanup()
        {
            _inputActions.Movment.Disable();
            _inputActions.Movment.Move.performed -= OnStartMove;
        }

        private void OnEndMove(InputAction.CallbackContext _) =>
            EndMove.Invoke();

        private void OnStartMove(InputAction.CallbackContext _) =>
            StartMove.Invoke();
    }
}