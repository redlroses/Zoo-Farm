using System;

namespace CodeBase.Services.Input
{
    public interface IPlayerInputService : IService, IInput
    {
        void Subscribe();
        void Cleanup();
        event Action StartMove;
        event Action EndMove;
    }
}