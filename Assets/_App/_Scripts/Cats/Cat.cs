using System;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetOnWorkPlace(Vector3 positionToPlace)
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        //_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transform.position = positionToPlace;
    }
}
