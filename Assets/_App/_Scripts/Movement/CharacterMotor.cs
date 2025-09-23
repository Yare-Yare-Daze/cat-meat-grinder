using System;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    [SerializeField] private CharacterConfig _config;

    private Transform _cameraTR;
    private CharacterController _characterController;
    private Vector3 _velocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraTR = Camera.main.transform;
    }

    public void Tick(Vector2 moveInput, bool sprintHeld, bool jumpPressed)
    {
        if (_characterController.isGrounded && _velocity.y < 0)
            _velocity.y = _config.groundedSnap;

        var forward = Vector3.forward;
        var right = Vector3.right;

        if (_cameraTR != null)
        {
            forward = _cameraTR.forward;
            forward.y = 0;
            forward.Normalize();
            
            right = _cameraTR.right;
            right.y = 0;
            right.Normalize();
        }
        
        var move = forward * moveInput.y + right * moveInput.x;
        if(move.sqrMagnitude > 1f) move.Normalize();

        var speed = _config.moveSpeed * (sprintHeld ? _config.sprintMultiplier : 1f);
        _characterController.Move(move * (speed * Time.deltaTime));

        if (jumpPressed && _characterController.isGrounded)
            _velocity.y = Mathf.Sqrt(_config.jumpHeight * -2f * _config.gravity);
        
        _velocity.y += _config.gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

        if (move.sqrMagnitude > 0.0001f)
        {
            Quaternion target = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, _config.rotationLerp * Time.deltaTime);
        }
    }
}
