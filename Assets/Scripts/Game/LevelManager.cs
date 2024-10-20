using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy1Prefab;
    [SerializeField] private GameObject _enemy2Prefab;
    [SerializeField] private GameObject _enemy3Prefab;
    [SerializeField] private GameObject _enemy4Prefab;
    [SerializeField] private EnemySpawningManager _enemySpawningManager;
    public int Level = 1;
    private int _enemy1Count = 0; // Purple
    private int _enemy2Count = 1; // Green
    private int _enemy3Count = 5; // Flying
    private int _enemy4Count = 0; // Big
    private int _enemy1Remaining;
    private int _enemy2Remaining;
    private int _enemy3Remaining;
    private int _enemy4Remaining;
    public UnityEvent OnLevelChanged;
    private int _totalRemaining
    {
        get
        {
            return _enemy1Remaining + _enemy2Remaining + _enemy3Remaining + _enemy4Remaining;
        }
    }
    private int _totalCount
    {
        get
        {
            return _enemy1Count + _enemy2Count + _enemy3Count + _enemy4Count;
        }
    }
    public float RatioSpawned
    {
        get
        {
            return (_totalCount - _totalRemaining) / (float)_totalCount;
        }
    }
    private float _enemySpawnDelay = 1.5f;
    private void Start()
    {
        SetRemainingToCount();
        _enemySpawningManager.SpawnDelay = _enemySpawnDelay;
    }
    private void Update()
    {
        if (_totalRemaining == 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            NextLevel();
        }
    }
    private void NextLevel()
    {
        Level++;
        _enemy2Count++;
        _enemy3Count++;
        _enemySpawnDelay *= 0.9f;
        if (Level >= 3)
        {
            _enemy1Count = Random.Range(Level - 5, Level);
            _enemy1Count = Mathf.Max(0, _enemy1Count);
        }
        if (Level >= 5)
        {
            _enemy4Count = Random.Range((Level - 7) / 2, Level / 2);
            _enemy4Count = Mathf.Max(0, _enemy4Count);
        }
        SetRemainingToCount();
        _enemySpawningManager.SpawnDelay = _enemySpawnDelay;
        OnLevelChanged.Invoke();
    }
    private void SetRemainingToCount()
    {
        _enemy1Remaining = _enemy1Count;
        _enemy2Remaining = _enemy2Count;
        _enemy3Remaining = _enemy3Count;
        _enemy4Remaining = _enemy4Count;
    }
    public GameObject GetEnemyToSpawn()
    {
        if (_totalRemaining == 0) return null;

        int val = Random.Range(1, _totalRemaining);
        if (val <= _enemy1Remaining)
        {
            _enemy1Remaining--;
            return _enemy1Prefab;
        }
        if (val <= _enemy1Remaining + _enemy2Remaining)
        {
            _enemy2Remaining--;
            return _enemy2Prefab;
        }
        if (val <= _enemy1Remaining + _enemy2Remaining + _enemy3Remaining)
        {
            _enemy3Remaining--;
            return _enemy3Prefab;
        }
        _enemy4Remaining--;
        return _enemy4Prefab;
    }
}
