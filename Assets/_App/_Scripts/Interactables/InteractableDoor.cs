using UnityEngine;

public class InteractableDoor : ItemInteractable
{
    [SerializeField] private PlayerWorkshop _playerWorkshop;

    private bool _isTeleported;
    
    public override void Interact()
    {
        base.Interact();
        
        _isTeleported = !_isTeleported;
        if (_isTeleported)
        {
            _playerWorkshop.ActivateWorkshop();
        }
        else
        {
            _playerWorkshop.DeactivateWorkshop();
        }
    }
}
