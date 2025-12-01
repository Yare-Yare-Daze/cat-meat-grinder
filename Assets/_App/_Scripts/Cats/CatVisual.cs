using System;
using System.Collections.Generic;
using UnityEngine;

public class CatVisual : MonoBehaviour
{
    [SerializeField] private List<Material> _catMaterials;
    [SerializeField] private Cat _cat;
    [SerializeField] private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _cat.OnCatTypeChange += OnCatTypeChangeHandler;
    }

    private void OnCatTypeChangeHandler(CatType newCatType)
    {
        _meshRenderer.material = _catMaterials[(int)newCatType];
    }
}
