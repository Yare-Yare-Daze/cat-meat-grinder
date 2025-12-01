using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatBuyPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _catImage;
    [SerializeField] private Button _chooseButton;
    
    //public Button.ButtonClickedEvent ChooseButtonClickedEvent => _chooseButton.onClick;
    
    public event Action<CatType> OnCatTypeSelected; 
    
    private CatType _catType;
    
    private void Awake()
    {
        _chooseButton.onClick.AddListener(OnButtonCatSelectClicked);
    }

    public void SetCatInfo(int index, Sprite catSprite, CatType catType)
    {
        _nameText.text = $"Cat {index}";
        _catImage.sprite = catSprite;
        _catType = catType;
    }

    private void OnButtonCatSelectClicked()
    {
        OnCatTypeSelected?.Invoke(_catType);
    }
}
