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
    [SerializeField] private CatType _catType;
    
    [Inject] private GameManager _gameManager;
    
    private Rigidbody _rigidbody;
    
    public event Action<CatType> OnCatTypeChange; 

    public CatType CatType
    {
        get => _catType;
        private set
        {
            _catType = value;
            OnCatTypeChange?.Invoke(CatType);
        }
    }

    private void Awake()
    {
        Initialize();
        SetCatType(_catType);
    }

    public void SetCatType(CatType newCatType)
    {
        CatType = newCatType;
    }

    private void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetOnWorkPlace(Transform workPlace)
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        //_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transform.position = workPlace.position;
        transform.parent = workPlace;
    }
}
