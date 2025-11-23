using UnityEngine;

public class ItemInteractable : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log($"{name} Interact");
    }

    public virtual void StopInteract()
    {
        Debug.Log($"{name} StopInteract");
    }
}
