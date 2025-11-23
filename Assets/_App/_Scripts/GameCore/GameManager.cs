using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Game Events")]
    public UnityEvent OnLoaded;
    public UnityEvent OnGameStarted;
    public UnityEvent OnGameEnded;
    public UnityEvent OnGamePaused;
    public UnityEvent OnGameUnpaused;

    private Queue<GameState> _queueStatesToChange;
    private bool _isBusy;

    public bool IsBusy
    {
        get => _isBusy;
        private set
        {
            _isBusy = value;
        }
    }

    private void Loaded() { OnLoaded?.Invoke(); }
    private void GameStarted() { OnGameStarted?.Invoke(); }
    private void GameEnded() { OnGameEnded?.Invoke(); }
    private void GamePaused() { OnGamePaused?.Invoke(); }
    private void GameUnpaused() { OnGameUnpaused?.Invoke(); }
    
    private bool _isGamePaused;
    private GameStateMachine _gameStateMachine;

    private void Awake()
    {
        Time.timeScale = 1f;
        Initialize();
    }

    private void Initialize()
    {
        _gameStateMachine = new GameStateMachine();
        _queueStatesToChange = new Queue<GameState>();

        SubscribeIntoEvents();
    }

    private void OnEnable()
    { 
        if(_gameStateMachine != null) SubscribeIntoEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeIntoEvents()
    {
        _gameStateMachine.OnCurrentGameStateChanged += OnCurrentGameStateChangedHandler;
    }

    private void UnsubscribeFromEvents()
    {
        _gameStateMachine.OnCurrentGameStateChanged -= OnCurrentGameStateChangedHandler;
    }

    private void OnCurrentGameStateChangedHandler(GameState newState)
    {
        IsBusy = false;

        if (_queueStatesToChange.Count > 0)
        {
            Debug.Log("Queue of states is not empty, dequeuing...");
            var dequeuedState = _queueStatesToChange.Dequeue();
            UpdateCurrentState(dequeuedState);
        }
    }

    private void UpdateCurrentState(GameState newState)
    {
        if (IsBusy)
        {
            _queueStatesToChange.Enqueue(newState);
            Debug.Log($"Enqueued state is {newState}");
            return;
        }
        
        IsBusy = true;
        //Debug.Log($"Current state is {newState}");

        switch (newState)
        {
            case GameState.Off:
                break;
            
            case GameState.Loaded:
                if (_gameStateMachine.CurrentGameState != GameState.Off)
                {
                    Debug.LogWarning($"State Loaded can set only from {GameState.Off}");
                    break;
                }
                
                Loaded();
                break;
            
            case GameState.GameStarted:
                if (_gameStateMachine.CurrentGameState != GameState.Loaded || _gameStateMachine.CurrentGameState != GameState.GameStarted)
                {
                    Debug.LogWarning($"State Loaded can set only from {GameState.Off}");
                    break;
                }
                
                GameStarted();
                break;
            
            case GameState.GameEnded:
                GameEnded();
                break;
            
            case GameState.GamePaused:
                GamePaused();
                break;
            
            case GameState.GameUnpaused:
                GameUnpaused();
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        _gameStateMachine.TransitionToState(newState);
    }
}
