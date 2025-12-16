using System;
using System.Collections.Generic;
using UnityEngine;

public class CatVisual : MonoBehaviour
{
    [SerializeField] private List<Material> _catMaterials;
    [SerializeField] protected Cat _cat;
    [SerializeField] protected MeshRenderer _meshRenderer;

    private void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _cat.OnCatTypeChange += OnCatTypeChangeHandler;
    }

    protected virtual void OnCatTypeChangeHandler(CatType newCatType)
    {
        _meshRenderer.material = _catMaterials[(int)newCatType];
    }
}
