using System;
using System.Collections;
using UnityEngine;

public class CatsSpawner : MonoBehaviour
{
    [SerializeField] private float _timeToAbleSpawn;
    [SerializeField] private GameObject _catPrefab;
    [SerializeField] private Transform _spawnPointTR;

    private bool _canSpawn;
    private float _timer;
    
    public float TimeToAbleSpawn => _timeToAbleSpawn;
    
    public event Action<float> OnTimerValueChanged; 

    public float Timer
    {
        get => _timer;
        private set
        {
            _timer = value;
            OnTimerValueChanged?.Invoke(_timer);
        }
    }
    
    private void Awake()
    {
        Timer = TimeToAbleSpawn;
        _canSpawn = true;
    }

    private IEnumerator TimerToAbleSpawn()
    {
        Timer = 0f;
        _canSpawn = false;
        
        while (Timer < _timeToAbleSpawn)
        {
            Timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        _canSpawn = true;
    }

    public void SpawnNewCat()
    {
        if(!_canSpawn) return;
        
        var newCat = Instantiate(_catPrefab, _spawnPointTR);
        newCat.transform.SetParent(null);
        StartCoroutine(TimerToAbleSpawn());
    }

    public void SpawnNewCat(CatType catType)
    {
        if(!_canSpawn) return;
        
        var newCat = Instantiate(_catPrefab, _spawnPointTR);
        newCat.transform.SetParent(null);
        var cat =  newCat.GetComponent<Cat>();
        cat.SetCatType(catType);
        StartCoroutine(TimerToAbleSpawn());
    }
}
