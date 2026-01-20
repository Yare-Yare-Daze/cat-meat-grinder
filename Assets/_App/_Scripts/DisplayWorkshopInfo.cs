using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DisplayWorkshopInfo : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshPro _itemsCountText;
    [SerializeField] private TextMeshPro _efficiencyText;
    [SerializeField] private TextMeshPro _catsText;
    
    [Header("Other")]
    [SerializeField] private PlayerWorkshop _playerWorkshop;
    [SerializeField] private Light _workStatusLight;
    [SerializeField] private List<MeshRenderer> _doorsMeshRenderers;
    
    private Color _initialDoorColor;

    private void Awake()
    {
        _initialDoorColor = _doorsMeshRenderers[0].material.color;
        
        _playerWorkshop.OnCountProductChanged += OnCountProductChangedHandler;
        _playerWorkshop.OnIsBusyChanged += OnIsBusyChangedHandler;
        _playerWorkshop.OnEfficiencyChanged += OnEfficiencyChangedHandler;
        _playerWorkshop.OnCatTypeChanged += OnCatTypeChangedHandler;

        OnIsBusyChangedHandler(WorkshopBusyState.None);
    }

    private void OnCatTypeChangedHandler(CatType catType)
    {
        switch (catType)
        {
            case CatType.Black:
                SetDoorsInColor(Color.black);
                //_workStatusLight.color = Color.red;
                break;
            case CatType.White:
                SetDoorsInColor(Color.white);
                //_workStatusLight.color = Color.white;
                break;
            case CatType.Orange:
                SetDoorsInColor(Color.orange);
                //_workStatusLight.color = Color.orange;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(catType), catType, null);
        }
    }

    private void SetDoorsInColor(Color color)
    {
        foreach (var doorMR in _doorsMeshRenderers)
        {
            doorMR.material.color = color;
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
    
    private void OnIsBusyChangedHandler(WorkshopBusyState newValue)
    {
        switch (newValue)
        {
            case WorkshopBusyState.None:
                _workStatusLight.enabled = false;
                SetDoorsInColor(_initialDoorColor);
                break;
            case WorkshopBusyState.Busy:
                _workStatusLight.enabled = true;
                _workStatusLight.color = Color.green;
                break;
            case WorkshopBusyState.Error:
                _workStatusLight.enabled = true;
                _workStatusLight.color = Color.red;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newValue), newValue, null);
        }
    }
}
