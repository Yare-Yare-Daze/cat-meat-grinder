using System;
using UnityEngine;
using Zenject;

public enum CatType
{
    Black,
    White,
    Orange
}

public class Cat : MonoBehaviour
{
    [Inject] private GameManager _gameManager;
    
    private CatType _catType;
    private Rigidbody _rigidbody;

    public CatType CatType
    {
        get => _catType;
        private set
        {
            _catType = value;
        }
    }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetOnWorkPlace(Vector3 positionToPlace)
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        //_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transform.position = positionToPlace;
    }
}
