using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

public class ComputerCanvas : MonoBehaviour
{
    [SerializeField] private List<Sprite> _catsTypeSprites;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Transform _catBuyPanelsMainTR;
    [SerializeField] private Slider _ableToSpawnSlider;
    
    [Inject] private CanvasesManager _canvasesManager;
    [Inject] private CatsSpawner _catsSpawner;
    
    private List<CatBuyPanel> _catBuyPanels = new List<CatBuyPanel>();

    private void Awake()
    {
        for (int i = 0; i < _catBuyPanelsMainTR.childCount; i++)
        {
            var tempCatBuyPanel = _catBuyPanelsMainTR.GetChild(i).GetComponent<CatBuyPanel>();
            _catBuyPanels.Add(tempCatBuyPanel);
            
            //_catBuyPanels[i].ChooseButtonClickedEvent.AddListener(());
            _catBuyPanels[i].OnCatTypeSelected += OnCatTypeSelectedHandler;
            _catBuyPanels[i].SetInteractableButton(true);
        }
        
        UpdateCatsInfo();

        _catsSpawner.OnTimerValueChanged += OnTimerValueChangedHandler;
    }

    private void OnTimerValueChangedHandler(float value)
    {
        _ableToSpawnSlider.value = value / _catsSpawner.TimeToAbleSpawn;
        
        if (_ableToSpawnSlider.value >= _ableToSpawnSlider.maxValue)
        {
            foreach (var catBuyPanel in _catBuyPanels)
            {
                catBuyPanel.SetInteractableButton(true);
            }
        }
        else
        {
            foreach (var catBuyPanel in _catBuyPanels)
            {
                catBuyPanel.SetInteractableButton(false);
            }
        }
    }

    private void UpdateCatsInfo()
    {
        for (int i = 0; i < _catBuyPanels.Count; i++)
        {
            var randomCatType = Random.Range(0, (int)CatType.Orange + 1);
            var randomCatSprite = _catsTypeSprites[randomCatType];
            _catBuyPanels[i].SetCatInfo(i, randomCatSprite, (CatType)randomCatType);
        }
    }

    private void SpawnNewCat()
    {
        _catsSpawner.SpawnNewCat();
        DisableCanvas();
    }

    private void SpawnNewCatByType(CatType catType)
    {
        _catsSpawner.SpawnNewCat(catType);
        UpdateCatsInfo();
        DisableCanvas();
    }

    private void OnCatTypeSelectedHandler(CatType catType)
    {
        SpawnNewCatByType(catType);
    }

    private void DisableCanvas()
    {
        _canvasesManager.SetDefaultCanvas();
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(DisableCanvas);
        //if(_catBuyPanels.Count > 0) UpdateCatsInfo();
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(DisableCanvas);
    }
}
