using System;
using UnityEngine;

public enum GameState
{
    Off,
    Loaded,
    GameStarted,
    GameEnded,
    GamePaused,
    GameUnpaused
}

[Serializable]
public class GameStateMachine
{
    private GameState _currentGameState;

    public event Action<GameState> OnCurrentGameStateChanged;

    public GameState CurrentGameState
    {
        get => _currentGameState;
        private set
        {
            _currentGameState = value;
            OnCurrentGameStateChanged?.Invoke(_currentGameState);
            Debug.Log($"Current Game State: {_currentGameState}");
        }
    }

    private void SetState(GameState newState)
    {
        if (newState != CurrentGameState)
        {
            CurrentGameState = newState;
        }
        else
        {
            Debug.LogWarning("Current game state already enabled");
        }
    }

    public void TransitionToState(GameState newState)
    {
        SetState(newState);
    }
}
