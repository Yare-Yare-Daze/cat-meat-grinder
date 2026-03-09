using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class PickUpController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerRaycaster _raycaster;
    
    [Header("Pickup Settings")] 
    [SerializeField] private Transform _holdArea;
    
    [Header("Physics Parameters")] 
    [SerializeField] private float _pickupForce = 150.0f;
    
    [Inject] private Player _player;
    
    private ItemSelectable _currentItemSelectable;
    private bool _teleportedFreeze;

    public ItemSelectable CurrentItemSelectable => _currentItemSelectable;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentItemSelectable == null)
            {
                if (_raycaster.TryGetComponentFromAllHits(out ItemSelectable itemSelectable))
                {
                    PickUpSelectableObject(itemSelectable);
                }
            }
            else
            {
                if(_player.PlayerInteract.CanInteract == false) DropObject();
            }
        }

        if (Input.GetMouseButtonDown(1) && _currentItemSelectable != null)
        {
            ThrowObject();
        }
    }

    private void FixedUpdate()
    {
        if (_currentItemSelectable != null && !_teleportedFreeze)
        {
            MoveObject();
        }
    }

    public void SetItemSelectableFromOutside(ItemSelectable itemSelectable)
    {
        if(_currentItemSelectable != null) return;
        
        Debug.Log($"Get item to {this.name}, and set to position");
        
        PickUpSelectableObject(itemSelectable);
        _currentItemSelectable.TeleportToPosition(_holdArea.position);

        //StartCoroutine(TeleportedFreeze());
    }

    // private IEnumerator TeleportedFreeze()
    // {
    //     _teleportedFreeze = true;
    //     yield return new WaitForSeconds(1.1f);
    //     _teleportedFreeze = false;
    // }

    public void TransferItemSelectableToNewPickUp(PickUpController newPickUpController)
    {
        Debug.Log($"Transfer item from {this.name} to {newPickUpController.name}");
        if(_currentItemSelectable == null) return;
        
        newPickUpController.SetItemSelectableFromOutside(_currentItemSelectable);
        
        DropObject();
    }

    private void MoveObject()
    {
        if (Vector3.Distance(_currentItemSelectable.transform.position, _holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (_holdArea.position - _currentItemSelectable.transform.position);
            //_currentItemSelectable.ItemRigidbody.AddForce(moveDirection * _pickupForce);
            _currentItemSelectable.ItemRigidbody.position = Vector3.Lerp(_currentItemSelectable.transform.position, _holdArea.position, Time.deltaTime * 10f);
        }
    }

    private void PickUpSelectableObject(ItemSelectable itemSelectable)
    {
        Debug.Log("Picked up object " + itemSelectable.name);
        _currentItemSelectable = itemSelectable;
        var rb = _currentItemSelectable.ItemRigidbody;
        
        rb.useGravity = false;
        rb.linearDamping = 10;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        _currentItemSelectable.transform.parent = _holdArea;
    }

    private void PickupObject(GameObject pickupObject)
    {
        if (pickupObject.TryGetComponent(out ItemSelectable itemSelectable))
        {
            _currentItemSelectable = itemSelectable;
            var rb = _currentItemSelectable.ItemRigidbody;
            rb.useGravity = false;
            rb.linearDamping = 10;
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            rb.transform.parent = _holdArea;
        }
    }
    
    private void DropObject()
    {
        Debug.Log("Dropped object " + _currentItemSelectable.name);
        var rb = _currentItemSelectable.ItemRigidbody;
        rb.useGravity = true;
        rb.linearDamping = 1;
        rb.constraints = RigidbodyConstraints.None;

        _currentItemSelectable.transform.parent = null;
        _currentItemSelectable = null;
    }

    private void ThrowObject()
    {
        var rb = _currentItemSelectable.ItemRigidbody;
        rb.useGravity = true;
        rb.linearDamping = 1;
        rb.constraints = RigidbodyConstraints.None;
        
        var throwVector = _currentItemSelectable.transform.position - transform.position;
        rb.AddForce(throwVector * 10f, ForceMode.Impulse);
        
        _currentItemSelectable.transform.parent = null;
        _currentItemSelectable = null;
    }
}
