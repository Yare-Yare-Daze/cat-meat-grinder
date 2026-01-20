using System;
using UnityEngine;

public class InteractableWorkshopDoor : ItemInteractable
{
    [SerializeField] private PlayerWorkshop _playerWorkshop;
    //[SerializeField] private MeshRenderer _doorMeshRenderer;
    
    private bool _isTeleported;

    // private void OnEnable()
    // {
    //     _playerWorkshop.OnCatTypeChanged += OnCatTypeChangedHandler;
    // }
    //
    // private void OnDisable()
    // {
    //     _playerWorkshop.OnCatTypeChanged -= OnCatTypeChangedHandler;
    // }

    public override void Interact()
    {
        base.Interact();
        
        _isTeleported = true;
        if (_isTeleported)
        {
            _playerWorkshop.ActivateWorkshop();
        }
    }

    // private void OnCatTypeChangedHandler(CatType catType)
    // {
    //     switch (catType)
    //     {
    //         case CatType.Black:
    //             _doorMeshRenderer.material.color = Color.black;
    //             break;
    //         case CatType.White:
    //             _doorMeshRenderer.material.color = Color.white;
    //             break;
    //         case CatType.Orange:
    //             _doorMeshRenderer.material.color = Color.orange;
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException(nameof(catType), catType, null);
    //     }
    // }

    public override void StopInteract()
    {
        base.StopInteract();
        _isTeleported = false;
        _playerWorkshop.DeactivateWorkshop();
    }
}
