using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera _playerMainCamera;

    public Camera PlayerMainCamera => _playerMainCamera;
    
    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
