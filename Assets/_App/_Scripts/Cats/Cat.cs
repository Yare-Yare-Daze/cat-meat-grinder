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
    [SerializeField] protected CatType _catType;
    
    [Inject] protected GameManager _gameManager;
    
    protected Rigidbody _rigidbody;
    
    public event Action<CatType> OnCatTypeChange; 

    public CatType CatType
    {
        get => _catType;
        protected set
        {
            _catType = value;
            OnCatTypeChange?.Invoke(CatType);
        }
    }

    private void Awake()
    {
        Initialize();
    }

    public void SetCatType(CatType newCatType)
    {
        CatType = newCatType;
    }

    protected virtual void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
        SetCatType(_catType);
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

    public void ResetFromWorkPlace()
    {
        transform.parent = null;
    }
}
