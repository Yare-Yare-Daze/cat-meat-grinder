using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerRaycaster _raycaster;
    
    private bool _canInteract;
    private ItemInteractable _currentInteractable;

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
        
        if (Input.GetMouseButtonDown(0) && CanInteract)
        {
            _currentInteractable.Interact();
        }
    }
}
