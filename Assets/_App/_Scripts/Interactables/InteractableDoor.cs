using UnityEngine;

public class InteractableDoor : ItemInteractable
{
    [SerializeField] private PlayerWorkshop _playerWorkshop;

    private bool _isTeleported;
    
    public override void Interact()
    {
        base.Interact();
        
        _isTeleported = true;
        if (_isTeleported)
        {
            _playerWorkshop.ActivateWorkshop();
        }
    }

    public override void StopInteract()
    {
        base.StopInteract();
        _isTeleported = false;
        _playerWorkshop.DeactivateWorkshop();
    }
}
