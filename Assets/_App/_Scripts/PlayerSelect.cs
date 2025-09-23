using System;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    [SerializeField] private float _maxDistanceInteractable;
    
    private Camera _mainCamera;
    private ItemSelectable _currentItemSelectable;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var rayFromCamera = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(rayFromCamera, out RaycastHit hit, _maxDistanceInteractable) && _currentItemSelectable == null)
            {
                if (hit.collider.gameObject != null)
                {
                    if (hit.collider.gameObject.TryGetComponent(out ItemSelectable itemSelectable))
                    {
                        _currentItemSelectable = itemSelectable;
                        Debug.Log(hit.collider.gameObject);
                    }
                }
            }
            
            
        }
    }
}
