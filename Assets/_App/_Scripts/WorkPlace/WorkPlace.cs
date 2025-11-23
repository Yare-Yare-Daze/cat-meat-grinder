using System;
using System.Collections;
using UnityEngine;

public class WorkPlace : MonoBehaviour
{
    [SerializeField] private Transform _workPlaceTR;

    [Header("Work params")] 
    [SerializeField] private float _timePerItem;

    private Cat _currentWorkingCat;
    private bool _isWorking;

    private int _itemsProduced;
    private float _currentTime = 0f;

    public event Action<bool> OnIsWorkingChanged;
    public event Action<int> OnItemsProducedChanged; 
    public event Action OnNewItemsProduced;

    public int ItemsProduced
    {
        get { return _itemsProduced; }
        private set
        {
            _itemsProduced = value;
            Debug.Log($"Work place {name} produced items changed: {_itemsProduced}.");
            OnItemsProducedChanged?.Invoke(_itemsProduced);
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
            ItemsProduced++;
            OnNewItemsProduced.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cat cat))
        {
            if (_currentWorkingCat == null)
            {
                _currentWorkingCat = cat;
                _currentWorkingCat.SetOnWorkPlace(_workPlaceTR.position);

                StartCoroutine(WaitToStopWork());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Cat cat))
        {
            _currentWorkingCat = null;
            StartCoroutine(WaitToWork());
        }
    }

    private IEnumerator WaitToWork()
    {
        yield return new WaitUntil(() => _currentWorkingCat != null);
        IsWorking = true;
        //StartCoroutine(Working());
    }

    private IEnumerator WaitToStopWork()
    {
        yield return new WaitUntil(() => _currentWorkingCat == null);
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
                ItemsProduced++;
            }
        }
        
        Debug.Log($"Work place {name} stopped working.");
    }
}
