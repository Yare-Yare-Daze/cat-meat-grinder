using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ItemSelectable : MonoBehaviour
{
    //private Rigidbody _rigidbody;
    
    public Rigidbody ItemRigidbody { private set; get; }

    private void Awake()
    {
        ItemRigidbody = GetComponent<Rigidbody>();
    }

    public void TeleportToPosition(Vector3 position)
    {
        ItemRigidbody.position = position;
    }
}
