using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerRaycaster _raycaster;
    
    private bool _canInteract;
    private ItemInteractable _currentInteractable;
    private ItemInteractable _activatedItemInteractable;
    
    private bool _isInteracting;

    public bool IsInteracting
    {
        get => _isInteracting;
        private set
        {
            _isInteracting = value;
            OnIsInteractingChanged?.Invoke(value);
        }
    }

    public event Action<bool> OnIsInteractingChanged;

    public bool CanInteract
    {
        get => _canInteract;
        private set
        {
            _canInteract = value;
        }
    }

    private void Update()
    {
        if (_activatedItemInteractable != null && Input.GetKeyDown(KeyCode.Escape))
        {
            _activatedItemInteractable.OnStopInteract -= DisableInteract;
            _activatedItemInteractable.StopInteract();
            _activatedItemInteractable = null;
            IsInteracting = false;
        }
        
        if (!_raycaster.gameObject.activeSelf)
        {
            CanInteract = false;
            _currentInteractable = null;
            return;
        }
        
        if (_raycaster.TryGetRaycastHit(out var hit))
        {
            if (hit.transform.gameObject.TryGetComponent(out ItemInteractable itemInteractable))
            {
                CanInteract = true;
                _currentInteractable = itemInteractable;
            }
            else
            {
                CanInteract = false;
                _currentInteractable = null;
            }
        }
        
        if (_activatedItemInteractable == null && Input.GetMouseButtonDown(0) && CanInteract)
        {
            _currentInteractable.Interact();
            _activatedItemInteractable = _currentInteractable;
            _activatedItemInteractable.OnStopInteract += DisableInteract;
            IsInteracting = true;
        }
    }

    private void DisableInteract()
    {
        _activatedItemInteractable.OnStopInteract -= DisableInteract;
        _activatedItemInteractable = null;
        IsInteracting = false;
    }
}
