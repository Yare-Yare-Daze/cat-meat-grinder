using System;
using System.Collections.Generic;
using UnityEngine;

public class CatsAmountContainer : MonoBehaviour
{
    private int _totalCatsAmount;
    private List<Cat> _allCats = new List<Cat>();
    
    public event Action<int> OnCatsAmountChanged;

    public int TotalCatsAmount
    {
        get { return _totalCatsAmount; }
        set
        {
            _totalCatsAmount = Mathf.Clamp(value, 0, int.MaxValue);
            OnCatsAmountChanged?.Invoke(_totalCatsAmount);
        }
    }

    private void Awake()
    {
        
    }

    public void AddCat(Cat cat)
    {
        TotalCatsAmount++;
        _allCats.Add(cat);
    }

    public void RemoveCat(Cat cat)
    {
        TotalCatsAmount--;
        _allCats.Remove(cat);
    }
}
