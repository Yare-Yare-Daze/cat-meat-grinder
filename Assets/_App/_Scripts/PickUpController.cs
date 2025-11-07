using System;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerRaycaster _raycaster;
    
    [Header("Pickup Settings")] 
    [SerializeField] private Transform _holdArea;
    
    private GameObject _heldObject;
    private Rigidbody _heldObjectRB;

    [Header("Physics Parameters")] 
    //[SerializeField] private float _pickupRange = 5.0f;
    [SerializeField] private float _pickupForce = 150.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_heldObject == null)
            {
                if (_raycaster.TryGetComponentFromAllHits(out ItemSelectable itemSelectable))
                {
                    PickUpSelectableObject(itemSelectable);
                }
                
                // RaycastHit hit;
                // if (_raycaster.TryGetRaycastHit(out hit))
                // {
                //     PickupObject(hit.transform.gameObject);
                // }
            }
            else
            {
                DropObject();
            }
        }

        if (Input.GetMouseButtonDown(1) && _heldObject != null)
        {
            ThrowObject();
        }

        if (_heldObject != null)
        {
            MoveObject();
        }

        // if (Input.GetMouseButtonUp(0) && _heldObject != null)
        // {
        //     DropObject();
        // }
        
        
    }

    private void MoveObject()
    {
        if (Vector3.Distance(_heldObject.transform.position, _holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (_holdArea.position - _heldObject.transform.position);
            _heldObjectRB.AddForce(moveDirection * _pickupForce);
        }
    }

    private void PickUpSelectableObject(ItemSelectable itemSelectable)
    {
        Debug.Log("Picked up object " + itemSelectable.name);
        _heldObjectRB = itemSelectable.ItemRigidbody;
        _heldObjectRB.useGravity = false;
        _heldObjectRB.linearDamping = 10;
        _heldObjectRB.constraints = RigidbodyConstraints.FreezeRotation;

        _heldObjectRB.transform.parent = _holdArea;
        _heldObject = itemSelectable.gameObject;
    }

    private void PickupObject(GameObject pickupObject)
    {
        if (pickupObject.TryGetComponent(out ItemSelectable itemSelectable))
        {
            _heldObjectRB = itemSelectable.ItemRigidbody;
            _heldObjectRB.useGravity = false;
            _heldObjectRB.linearDamping = 10;
            _heldObjectRB.constraints = RigidbodyConstraints.FreezeRotation;

            _heldObjectRB.transform.parent = _holdArea;
            _heldObject = pickupObject;
        }
    }
    
    private void DropObject()
    {
        _heldObjectRB.useGravity = true;
        _heldObjectRB.linearDamping = 1;
        _heldObjectRB.constraints = RigidbodyConstraints.None;

        _heldObjectRB.transform.parent = null;
        _heldObject = null;
    }

    private void ThrowObject()
    {
        _heldObjectRB.useGravity = true;
        _heldObjectRB.linearDamping = 1;
        _heldObjectRB.constraints = RigidbodyConstraints.None;
        
        var throwVector = _heldObject.transform.position - transform.position;
        _heldObjectRB.AddForce(throwVector * 10f, ForceMode.Impulse);
        
        _heldObjectRB.transform.parent = null;
        _heldObject = null;
    }
}
