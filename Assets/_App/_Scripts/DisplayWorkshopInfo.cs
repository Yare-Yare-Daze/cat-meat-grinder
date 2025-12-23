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

    private void Awake()
    {
        _playerWorkshop.OnCountProductChanged += OnCountProductChangedHandler;
        _playerWorkshop.OnIsBusyChanged += OnIsBusyChangedHandler;
        _playerWorkshop.OnEfficiencyChanged += OnEfficiencyChangedHandler;
        _playerWorkshop.OnCatTypeChanged += OnCatTypeChangedHandler;

        OnIsBusyChangedHandler(false);
    }

    private void OnCatTypeChangedHandler(CatType catType)
    {
        switch (catType)
        {
            case CatType.Black:
                _pointLight.color = Color.red;
                break;
            case CatType.White:
                _pointLight.color = Color.white;
                break;
            case CatType.Orange:
                _pointLight.color = Color.orange;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(catType), catType, null);
        }
    }

    private void OnEfficiencyChangedHandler(float newEfficiency)
    {
        _efficiencyText.text = $"Efficiency: {newEfficiency.ToString("f2")}";
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
        }
        else
        {
            _pointLight.enabled = false;
        }
    }
}
