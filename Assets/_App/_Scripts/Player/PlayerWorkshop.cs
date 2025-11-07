using System;
using UnityEngine;

public class PlayerWorkshop : MonoBehaviour
{
    [SerializeField] private Camera _teleportedCamera;

    public void ActivateWorkshop()
    {
        Debug.Log("ActivateWorkshop");
        Player.Instance.PlayerMainCamera.gameObject.SetActive(false);
        _teleportedCamera.gameObject.SetActive(true);
    }

    public void DeactivateWorkshop()
    {
        Debug.Log("DeactivateWorkshop");
        Player.Instance.PlayerMainCamera.gameObject.SetActive(true);
        _teleportedCamera.gameObject.SetActive(false);
    }
}
