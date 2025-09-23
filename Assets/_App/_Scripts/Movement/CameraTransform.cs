using System;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;

    private void Update()
    {
        transform.position = _cameraPosition.position;
    }
}
