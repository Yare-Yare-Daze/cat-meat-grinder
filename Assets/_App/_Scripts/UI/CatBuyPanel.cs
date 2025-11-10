using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatBuyPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _catImage;
    [SerializeField] private Button _chooseButton;
    
    public Button.ButtonClickedEvent ChooseButtonClickedEvent => _chooseButton.onClick;
    
    private void Awake()
    {
        
    }

    public void SetCatInfo(int index)
    {
        _nameText.text = $"Cat {index}";
    }
}
