using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ComputerCanvas : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Transform _catBuyPanelsMainTR;
    
    [Inject] private CanvasesManager _canvasesManager;
    [Inject] private CatsSpawner _catsSpawner;
    
    private List<CatBuyPanel> _catBuyPanels = new List<CatBuyPanel>();

    private void Awake()
    {
        for (int i = 0; i < _catBuyPanelsMainTR.childCount; i++)
        {
            var tempCatBuyPanel = _catBuyPanelsMainTR.GetChild(i).GetComponent<CatBuyPanel>();
            _catBuyPanels.Add(tempCatBuyPanel);
            _catBuyPanels[i].SetCatInfo(i);
            _catBuyPanels[i].ChooseButtonClickedEvent.AddListener(SpawnNewCat);
        }
    }

    private void SpawnNewCat()
    {
        _catsSpawner.SpawnNewCat();
        _canvasesManager.SetDefaultCanvas();
    }

    private void DisableCanvas()
    {
        _canvasesManager.SetDefaultCanvas();
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(DisableCanvas);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(DisableCanvas);
    }
}
