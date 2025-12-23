using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera _playerMainCamera;
    [SerializeField] private PickUpController _playerPickUpController;
    [SerializeField] private PlayerInteract _playerInteract;
    [SerializeField] private PlayerMovement _playerMovement;

    public PickUpController PickUpController => _playerPickUpController;
    public Camera PlayerMainCamera => _playerMainCamera;
    public PlayerInteract PlayerInteract => _playerInteract;
    public PlayerMovement PlayerMovement => _playerMovement;

    private void Awake()
    {
        
    }
}
