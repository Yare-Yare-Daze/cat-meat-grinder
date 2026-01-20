using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum WorkshopBusyState
{
    None,
    Busy,
    Error
}

public class PlayerWorkshop : MonoBehaviour
{
    [SerializeField] private Camera _teleportedCamera;
    [SerializeField] private PickUpController _workshopPickUpController;

    [Inject] private Player _player;
    
    [SerializeField] private WorkPlace _workPlace;

    private int _countProduct;
    private bool _isBusy;
    private float _efficiency;
    private WorkshopBusyState _workshopState;

    public event Action<int> OnCountProductChanged;
    public event Action<WorkshopBusyState> OnIsBusyChanged;
    public event Action<CatType> OnCatTypeChanged; 
    
    public event Action<float> OnEfficiencyChanged;

    public WorkshopBusyState WorkshopState
    {
        get { return _workshopState; }
        private set
        {
            _workshopState = value;
            OnIsBusyChanged?.Invoke(value);
        }
    }

    // public bool IsBusy
    // {
    //     get => _isBusy;
    //     private set
    //     {
    //         _isBusy = value;
    //         OnIsBusyChanged?.Invoke(value);
    //     }
    // }

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
        // for (int i = 0; i < _workPlacesMainTR.childCount; i++)
        // {
        //     var workPlace = _workPlacesMainTR.GetChild(i).GetComponent<WorkPlace>();
        //     _workPlaces.Add(workPlace);
        //     _workPlaces[i].OnIsWorkingChanged += OnIsWorkingChangedHandler;
        //     //_workPlaces[i].OnItemsProducedChanged += OnItemsProducedChangedHandler;
        //     //_workPlaces[i].OnNewItemsProduced += OnNewItemsProducedHandler;
        // }

        _workPlace.OnEfficiencyChanged += OnEfficiencyChangedHandler;
        _workPlace.OnIsWorkingChanged += OnIsWorkingChangedHandler;
        _workPlace.OnCatTypeChanged += OnCatTypeChangedHandler;
    }

    private void OnCatTypeChangedHandler(CatType catType)
    {
        OnCatTypeChanged?.Invoke(catType);
    }

    private void OnEfficiencyChangedHandler(float newEfficiency)
    {
        OnEfficiencyChanged?.Invoke(newEfficiency);
    }

    private void OnIsWorkingChangedHandler(bool isWorking)
    {
        WorkshopState = isWorking ? WorkshopBusyState.Busy : WorkshopBusyState.None;
        //IsBusy = CheckIsWorkshopBusy();
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
        bool isBusy = _workPlace.IsWorking;

        return isBusy;
    }

    public void ActivateWorkshop()
    {
        Debug.Log("ActivateWorkshop");
        _player.PickUpController.TransferItemSelectableToNewPickUp(_workshopPickUpController);
        _player.PlayerMainCamera.gameObject.SetActive(false);
        _teleportedCamera.gameObject.SetActive(true);
    }

    public void DeactivateWorkshop()
    {
        Debug.Log("DeactivateWorkshop");
        _workshopPickUpController.TransferItemSelectableToNewPickUp(_player.PickUpController);
        _player.PlayerMainCamera.gameObject.SetActive(true);
        _teleportedCamera.gameObject.SetActive(false);
    }
}
