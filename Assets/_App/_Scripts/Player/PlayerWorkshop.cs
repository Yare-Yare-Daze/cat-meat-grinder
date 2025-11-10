using System;
using UnityEngine;
using Zenject;

public class PlayerWorkshop : MonoBehaviour
{
    [SerializeField] private Camera _teleportedCamera;

    [Inject] private Player _player;
    
    public void ActivateWorkshop()
    {
        Debug.Log("ActivateWorkshop");
        _player.PlayerMainCamera.gameObject.SetActive(false);
        _teleportedCamera.gameObject.SetActive(true);
    }

    public void DeactivateWorkshop()
    {
        Debug.Log("DeactivateWorkshop");
        _player.PlayerMainCamera.gameObject.SetActive(true);
        _teleportedCamera.gameObject.SetActive(false);
    }
}
