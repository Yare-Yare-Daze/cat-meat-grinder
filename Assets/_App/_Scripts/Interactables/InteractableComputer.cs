using System;
using UnityEngine;
using Zenject;

public class InteractableComputer : ItemInteractable
{
    [Inject] private CanvasesManager _canvasesManager;
    
    public override void Interact()
    {
        base.Interact();
        _canvasesManager.ActivateCanvas(CanvasType.ComputerCanvas);
        _canvasesManager.OnCanvasTypeChanged += OnCanvasTypeChangedHandler;
    }

    private void OnCanvasTypeChangedHandler(CanvasType type)
    {
        switch (type)
        {
            case CanvasType.PlayerCanvas:
                _canvasesManager.OnCanvasTypeChanged -= OnCanvasTypeChangedHandler;
                StopInteract();
                break;
            case CanvasType.ComputerCanvas:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}
