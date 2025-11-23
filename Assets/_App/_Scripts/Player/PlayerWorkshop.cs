using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerWorkshop : MonoBehaviour
{
    [SerializeField] private Camera _teleportedCamera;
    [SerializeField] private Transform _workPlacesMainTR;

    [Inject] private Player _player;
    
    private List<WorkPlace> _workPlaces = new List<WorkPlace>();

    private int _countProduct;
    private bool _isBusy;

    public event Action<int> OnCountProductChanged;
    public event Action<bool> OnIsBusyChanged;

    public bool IsBusy
    {
        get => _isBusy;
        private set
        {
            _isBusy = value;
            OnIsBusyChanged?.Invoke(value);
        }
    }

    public int CountProduct
    {
        get => _countProduct;
        private set
        {
            _countProduct = value;
            OnCountProductChanged?.Invoke(_countProduct);
        }
    }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < _workPlacesMainTR.childCount; i++)
        {
            var workPlace = _workPlacesMainTR.GetChild(i).GetComponent<WorkPlace>();
            _workPlaces.Add(workPlace);
            _workPlaces[i].OnIsWorkingChanged += OnIsWorkingChangedHandler;
            _workPlaces[i].OnItemsProducedChanged += OnItemsProducedChangedHandler;
            _workPlaces[i].OnNewItemsProduced += OnNewItemsProducedHandler;
        }
    }

    private void OnIsWorkingChangedHandler(bool isWorking)
    {
        IsBusy = CheckIsWorkshopBusy();
    }
    
    private void OnItemsProducedChangedHandler(int newValue)
    {
        
    }
    
    private void OnNewItemsProducedHandler()
    {
        CountProduct++;
    }

    private bool CheckIsWorkshopBusy()
    {
        bool isBusy = false;
        foreach (var workPlace in _workPlaces)
        {
            if (workPlace.IsWorking)
            {
                isBusy = true;
                continue;
            }
        }

        return isBusy;
    }

    public void ActivateWorkshop()
    {
        Debug.Log("ActivateWorkshop");
        _player.PlayerMainCamera.gameObject.SetActive(false);
        _teleportedCamera.gameObject.SetActive(true);
    }

    public void DeactivateWorkshop()
    {
        Debug.Log("DeactivateWorkshop");
        _player.PlayerMainCamera.gameObject.SetActive(true);
        _teleportedCamera.gameObject.SetActive(false);
    }
}
