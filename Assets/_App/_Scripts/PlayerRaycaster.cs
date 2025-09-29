using System;
using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    [SerializeField] private float _raycastDistance = 5.0f;
    
    private RaycastHit _hit;
    private bool _isHit = false;

    public RaycastHit ResultHit
    {
        get => _hit;
        private set
        {
            _hit = value;
        }
    }
    
    private void Update()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, _raycastDistance))
        {
            _isHit = true;
            ResultHit = _hit;
        }
        else
        {
            _isHit = false;
        }
    }

    public bool CheckRaycastHit(out RaycastHit hit)
    {
        hit = ResultHit;
        return _isHit;
    }
}
