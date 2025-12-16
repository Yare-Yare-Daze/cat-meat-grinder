using System;
using UnityEngine;

public class ItemInteractable : MonoBehaviour
{
    public event Action OnStartInteract;
    public event Action OnStopInteract;
    
    public virtual void Interact()
    {
        Debug.Log($"{name} Interact");
        OnStartInteract?.Invoke();
    }

    public virtual void StopInteract()
    {
        Debug.Log($"{name} StopInteract");
        OnStopInteract?.Invoke();
    }
}
