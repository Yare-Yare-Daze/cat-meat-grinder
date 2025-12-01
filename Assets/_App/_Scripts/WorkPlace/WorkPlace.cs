using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPlace : MonoBehaviour
{
    // [Header("Cat places")]
    // [SerializeField] private Transform _workPlaceBlack;
    // [SerializeField] private Transform _workPlaceWhite;
    // [SerializeField] private Transform _workPlaceOrange;

    [Header("Work params")] 
    [SerializeField] private float _timePerItem;
    [SerializeField] private float _baseEfficiency = 1f;
    
    private List<Cat> _workingCatsList = new List<Cat>(); 
    
    private List<Cat> _workingCatsBlacks = new List<Cat>();
    private List<Cat> _workingCatsWhites = new List<Cat>();
    private List<Cat> _workingCatsOranges = new List<Cat>();
    
    private bool _isWorking;

    //private int _itemsProduced;
    private float _currentTime = 0f;

    private float _totalEfficiency = 0f;

    private CatType _catTypeWorkPlace;

    public event Action<bool> OnIsWorkingChanged;
    public event Action<float> OnEfficiencyChanged; 
    //public event Action<int> OnItemsProducedChanged; 
    public event Action OnNewItemsProduced;

    // public int ItemsProduced
    // {
    //     get { return _itemsProduced; }
    //     private set
    //     {
    //         _itemsProduced = value;
    //         Debug.Log($"Work place {name} produced items changed: {_itemsProduced}.");
    //         OnItemsProducedChanged?.Invoke(_itemsProduced);
    //     }
    // }

    public float TotalEfficiency
    {
        get => _totalEfficiency;
        private set
        {
            _totalEfficiency = value;
            OnEfficiencyChanged?.Invoke(_totalEfficiency);
        }
    }
    
    public bool IsWorking
    {
        get { return _isWorking; }
        private set
        {
            _isWorking = value;
            Debug.Log($"Work place {name} working state changed: {_isWorking}.");
            OnIsWorkingChanged?.Invoke(_isWorking);
        }
    }

    private void Start()
    {
        StartCoroutine(WaitToWork());
    }

    private void Update()
    {
        if(!_isWorking) return;
        
        if (_currentTime < _timePerItem)
        {
            _currentTime += Time.deltaTime;
        }
        else
        {
            _currentTime = 0f;
            //ItemsProduced++;
            //OnNewItemsProduced.Invoke();
        }
    }

    private void AddNewCatAtWorkPlace(Cat cat)
    {
        if (_workingCatsList.Count == 0)
        {
            _catTypeWorkPlace = cat.CatType;
        }

        switch (cat.CatType)
        {
            case CatType.Black:
                
                _workingCatsBlacks.Add(cat);
                break;
            
            case CatType.White:
                
                _workingCatsWhites.Add(cat);
                break;
            
            case CatType.Orange:
                
                _workingCatsOranges.Add(cat);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        _workingCatsList.Add(cat);

        CountTotalEfficiency();
        if(!_isWorking) StartCoroutine(WaitToStopWork());
    }

    private void RemoveCatFromWorkPlace(Cat cat)
    {
        switch (cat.CatType)
        {
            case CatType.Black:
                _workingCatsBlacks.Remove(cat);
                break;
            case CatType.White:
                _workingCatsWhites.Remove(cat);
                break;
            case CatType.Orange:
                _workingCatsOranges.Remove(cat);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        CountTotalEfficiency();
        _workingCatsList.Remove(cat);
    }

    private void CountTotalEfficiency()
    {
        if (_workingCatsList.Count <= 0) return;
        
        TotalEfficiency = _baseEfficiency;
        var baseCats = 0;
        var positiveCats = 0;
        var negativeCats = 0;
        
        switch (_catTypeWorkPlace)
        {
            case CatType.Black:
                baseCats = _workingCatsBlacks.Count;
                positiveCats = _workingCatsOranges.Count;
                negativeCats = _workingCatsWhites.Count;
                break;
            
            case CatType.White:
                baseCats = _workingCatsWhites.Count;
                positiveCats = _workingCatsBlacks.Count;
                negativeCats = _workingCatsOranges.Count;
                break;
            
            case CatType.Orange:
                baseCats = _workingCatsOranges.Count;
                positiveCats = _workingCatsWhites.Count;
                negativeCats = _workingCatsBlacks.Count;
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        var positiveCatsNotPaired = Mathf.Abs(baseCats - positiveCats);
        var maxCountPositive = Mathf.Max(baseCats, positiveCats);
        TotalEfficiency += maxCountPositive - positiveCatsNotPaired;
        
        var negativeCatsNotPaired = Mathf.Abs(positiveCats - negativeCats);
        var maxCountNegative = Mathf.Max(positiveCats, negativeCats);
        TotalEfficiency -= maxCountNegative - negativeCatsNotPaired;
        
        Debug.Log($"Total efficiency: {TotalEfficiency}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cat cat))
        {
            AddNewCatAtWorkPlace(cat);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Cat cat))
        {
            RemoveCatFromWorkPlace(cat);
        }
    }

    private IEnumerator WaitToWork()
    {
        yield return new WaitUntil(() => _workingCatsList.Count > 0);
        IsWorking = true;
    }

    private IEnumerator WaitToStopWork()
    {
        yield return new WaitUntil(() => _workingCatsList.Count == 0);
        IsWorking = false;
    }

    private IEnumerator Working()
    {
        yield return new WaitUntil(() => _isWorking == true);

        Debug.Log($"Work place {name} started working.");
        
        var time = 0f;
        while (_isWorking)
        {
            if (time < _timePerItem)
            {
                time += Time.deltaTime;
            }
            else
            {
                time = 0f;
                //ItemsProduced++;
            }
        }
        
        Debug.Log($"Work place {name} stopped working.");
    }
}
