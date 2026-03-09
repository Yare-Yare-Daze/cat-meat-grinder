using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ItemSelectable : MonoBehaviour
{
    //private Rigidbody _rigidbody;
    
    public Rigidbody ItemRigidbody { private set; get; }
    private Collider _collider;

    private void Awake()
    {
        ItemRigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void TeleportToPosition(Vector3 position)
    {
        ItemRigidbody.position = position;
    
        ItemRigidbody.linearVelocity = Vector3.zero;
        ItemRigidbody.angularVelocity = Vector3.zero;

        StartCoroutine(TeleportedFreeze());
    }

    private IEnumerator TeleportedFreeze()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }
}
