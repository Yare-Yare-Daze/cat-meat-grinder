using System;
using UnityEngine;

public class CatsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _catPrefab;
    [SerializeField] private Transform _spawnPointTR;

    private void Awake()
    {
        
    }

    public void SpawnNewCat()
    {
        var newCat = Instantiate(_catPrefab, _spawnPointTR);
        newCat.transform.SetParent(null);
    }

    public void SpawnNewCat(CatType catType)
    {
        var newCat = Instantiate(_catPrefab, _spawnPointTR);
        newCat.transform.SetParent(null);
        var cat =  newCat.GetComponent<Cat>();
        cat.SetCatType(catType);
    }
}
