using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInput
    {
        Vector2 MoveDirection { get; }
    }
}