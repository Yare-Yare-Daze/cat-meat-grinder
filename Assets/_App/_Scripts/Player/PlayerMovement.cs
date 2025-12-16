using System; 
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterConfig _config;
    [SerializeField] private Transform _playerOrientation;

    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _groundDrag;
    [SerializeField] private float _airMultiplier;
    
    [Inject] private Player _player;

    private bool _canMove = true;
    private bool _isGrounded;
    private bool _readyToJump;
    
    private float _horizontal;
    private float _vertical;
    private Vector3 _moveDirection;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _player.PlayerInteract.OnIsInteractingChanged += OnIsInteractingChangedHandler;
        
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    private void OnIsInteractingChangedHandler(bool isInteracting)
    {
        _canMove = !isInteracting;
    }

    private void ReadInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            //_readyToJump = false;
            
            Jump();
        }
    }

    private void MovePlayer()
    {
        _moveDirection = _playerOrientation.forward * _vertical + _playerOrientation.right * _horizontal;

        if (_isGrounded)
        {
            _rigidbody.AddForce(_moveDirection.normalized * _config.moveSpeed, ForceMode.Force);
        }
        else
        {
            _rigidbody.AddForce(_moveDirection.normalized * _config.moveSpeed * _airMultiplier, ForceMode.Force);
        }
    }

    private void Update()
    {
        if(!_canMove) return;
        
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _ground);
        
        ReadInput();

        if (_isGrounded)
            _rigidbody.linearDamping = _groundDrag;
        else
            _rigidbody.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Jump()
    {
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);
        _rigidbody.AddForce(transform.up * _config.jumpHeight, ForceMode.Impulse);
    }
}
