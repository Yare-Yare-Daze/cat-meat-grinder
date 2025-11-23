using System;
using TMPro;
using UnityEngine;

public class DisplayWorkshopInfo : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshPro _itemsCountText;
    [SerializeField] private TextMeshPro _efficiencyText;
    [SerializeField] private TextMeshPro _catsText;
    
    [Header("Other")]
    [SerializeField] private PlayerWorkshop _playerWorkshop;
    [SerializeField] private Light _pointLight;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    private void Awake()
    {
        _playerWorkshop.OnCountProductChanged += OnCountProductChangedHandler;
        _playerWorkshop.OnIsBusyChanged += OnIsBusyChangedHandler;

        OnIsBusyChangedHandler(false);
    }

    private void OnCountProductChangedHandler(int newValue)
    {
        _itemsCountText.text = $"Items count: {newValue.ToString()}";
    }
    
    private void OnIsBusyChangedHandler(bool newValue)
    {
        if (newValue)
        {
            _pointLight.enabled = true;
            _pointLight.color = Color.green;
        }
        else
        {
            _pointLight.color = Color.white;
            _pointLight.enabled = false;
        }
    }
}
