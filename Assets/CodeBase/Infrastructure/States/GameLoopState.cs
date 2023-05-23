﻿using CodeBase.Services.Input;

namespace CodeBase.Infrastructure.States
{
  public class GameLoopState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly IPlayerInputService _inputService;

    public GameLoopState(GameStateMachine stateMachine,
      IPlayerInputService inputService)
    {
      _inputService = inputService;
      _stateMachine = stateMachine;
    }

    public void Exit() =>
      _inputService.Cleanup();

    public void Enter() =>
      _inputService.Subscribe();
  }
}