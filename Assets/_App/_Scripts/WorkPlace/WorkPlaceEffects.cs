using System;
using UnityEngine;

public class WorkPlaceEffects : MonoBehaviour
{
    [SerializeField] private Material _deactiveWorkPlaceMaterial;
    [SerializeField] private Material _activeWorkPlaceMaterial;
    [SerializeField] private Transform _meshseMain;
    
    private WorkPlace _workPlace;
    private MeshRenderer[] _meshRenderers;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _workPlace = GetComponent<WorkPlace>();
        _meshRenderers = _meshseMain.GetComponentsInChildren<MeshRenderer>();
        OnIsWorkingChangedHandler(false);
    }

    private void Start()
    {
        _workPlace.OnIsWorkingChanged += OnIsWorkingChangedHandler;
    }

    private void OnIsWorkingChangedHandler(bool isWorking)
    {
        if (isWorking)
        {
            foreach (var meshRenderer in _meshRenderers)
            {
                meshRenderer.material = _activeWorkPlaceMaterial;
            }
        }
        else
        {
            foreach (var meshRenderer in _meshRenderers)
            {
                meshRenderer.material = _deactiveWorkPlaceMaterial;
            }
        }
    }
}
