using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class CatsSpawner : MonoBehaviour
{
    [SerializeField] private float _baseTimeToAbleSpawn = 1f;
    [SerializeField] private GameObject _catPrefab;
    [SerializeField] private Transform _spawnPointTR;

    private bool _canSpawn;
    private float _timer;
    private float _targetTimeToSpawn;

    public float TargetTimeToSpawn
    {
        get { return _targetTimeToSpawn; }
        private set
        {
            _targetTimeToSpawn = Mathf.Clamp(value, 0f, float.MaxValue);
        }
    }
    
    [Inject] private CatsAmountContainer _amountContainer;
    
    public float BaseTimeToAbleSpawn => _baseTimeToAbleSpawn;
    
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
        TargetTimeToSpawn = _baseTimeToAbleSpawn;
        Timer = BaseTimeToAbleSpawn;
        _canSpawn = true;
        
        _amountContainer.OnCatsAmountChanged += OnCatsAmountChangedHandler;
    }
    
    private void OnCatsAmountChangedHandler(int amount)
    {
        TargetTimeToSpawn = _baseTimeToAbleSpawn + Mathf.Clamp(amount / 2f, 0f, 30f);
    }

    private IEnumerator TimerToAbleSpawn()
    {
        Timer = 0f;
        _canSpawn = false;
        
        while (Timer < TargetTimeToSpawn)
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
        
        _amountContainer.AddCat(newCat.GetComponent<Cat>());
    }

    public void SpawnNewCat(CatType catType)
    {
        if(!_canSpawn) return;
        
        var newCat = Instantiate(_catPrefab, _spawnPointTR);
        newCat.transform.SetParent(null);
        var cat =  newCat.GetComponent<Cat>();
        cat.SetCatType(catType);
        StartCoroutine(TimerToAbleSpawn());
        
        _amountContainer.AddCat(cat);
    }
}
