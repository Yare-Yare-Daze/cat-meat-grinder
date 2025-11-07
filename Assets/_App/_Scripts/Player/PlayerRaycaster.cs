using System;
using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    [SerializeField] private float _raycastDistance = 5.0f;
    
    private RaycastHit[] _raycastHits;
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
            _raycastHits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), _raycastDistance);
        }
        else
        {
            _isHit = false;
        }
    }

    public bool TryGetRaycastHit(out RaycastHit hit)
    {
        hit = ResultHit;
        return _isHit;
    }

    public bool TryGetAllHits(out RaycastHit[] hits)
    {
        hits = _raycastHits;
        return _isHit;
    }

    public bool TryGetComponentFromAllHits<T>(out T component)
    {
        for (int i = 0; i < _raycastHits.Length; i++)
        {
            var hit = _raycastHits[i];
            if (hit.collider.gameObject.TryGetComponent(out T component2))
            {
                component = component2;
                return true;
            }
        }

        component = default;
        return false;
    }
}
