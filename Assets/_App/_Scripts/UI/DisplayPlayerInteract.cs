using System;
using UnityEngine;

public class DisplayPlayerInteract : MonoBehaviour
{
    [SerializeField] private GameObject _interactDisplayGO;
    [SerializeField] private PlayerInteract _playerInteract;

    private void Update()
    {
        if (_playerInteract.CanInteract && !_interactDisplayGO.activeSelf)
        {
            _interactDisplayGO.SetActive(true);
        }
        else if(!_playerInteract.CanInteract && _interactDisplayGO.activeSelf)
        {
            _interactDisplayGO.SetActive(false);
        }
    }
}
