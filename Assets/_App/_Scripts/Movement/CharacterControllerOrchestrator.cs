using System;
using UnityEngine;

public class CharacterControllerOrchestrator : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _inputProvider;
    [SerializeField] private CharacterMotor _characterMotor;

    private IPlayerInput _playerInput;
    
    private void Awake()
    {
        _playerInput = (IPlayerInput)_inputProvider;
        if(_characterMotor == null) _characterMotor = GetComponent<CharacterMotor>();
    }

    private void Update()
    {
        var jump = _playerInput.ConsumeJump();
        _characterMotor.Tick(_playerInput.Move, _playerInput.SprintHeld, jump);
    }
}
