using UnityEngine;
using Zenject;

public class InteractableComputer : ItemInteractable
{
    [Inject] private CanvasesManager _canvasesManager;
    
    public override void Interact()
    {
        base.Interact();
        _canvasesManager.ActivateCanvas(CanvasType.ComputerCanvas);
    }
}
