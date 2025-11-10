using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera _playerMainCamera;

    public Camera PlayerMainCamera => _playerMainCamera;

    private void Awake()
    {
        
    }
}
