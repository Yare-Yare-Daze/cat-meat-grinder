using System;
using System.Collections.Generic;
using UnityEngine;

public enum CanvasType
{
    PlayerCanvas,
    ComputerCanvas
}

public class CanvasesManager : MonoBehaviour
{
    //[SerializeField] private GameObject _playerCanvas;
    //[SerializeField] private GameObject _computerCanvas;

    private List<GameObject> _canvases =  new List<GameObject>();

    private CanvasType _currentCanvasType;
    
    public event Action<CanvasType> OnCanvasTypeChanged; 

    public CanvasType CurrentCanvasType
    {
        get { return _currentCanvasType; }
        private set
        {
            _currentCanvasType = value;
            OnCanvasTypeChanged?.Invoke(_currentCanvasType);
        }
    }

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _canvases.Add(transform.GetChild(i).gameObject);
        }
    }

    public void ActivateCanvas(CanvasType canvasType)
    {
        DisableAllCanvases();
        _canvases[(int)canvasType].SetActive(true);
        CurrentCanvasType = canvasType;
    }

    public void SetDefaultCanvas()
    {
        ActivateCanvas(CanvasType.PlayerCanvas);
    }

    private void DisableAllCanvases()
    {
        foreach (GameObject canvas in _canvases)
            canvas.SetActive(false);
    }
}
