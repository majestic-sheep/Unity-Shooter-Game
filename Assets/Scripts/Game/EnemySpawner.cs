using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawningManager : MonoBehaviour
{
    public static EnemySpawningManager Instance { get; private set; }

    public float LevelSpawnDelay;
    public float SpawnDelayMultModifier;
    private float _lastSpawnTime;

    private const float SPAWNMARGIN = 15f;

    private bool _spawning;
    public bool Spawning
    {
        set
        {
            _spawning = value;
            if (value)
            {
                _lastSpawnTime = Time.time;
            }
        }
    }

    public UnityEvent OnEnemySpawned;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _lastSpawnTime = Time.time - LevelSpawnDelay;
        SpawnDelayMultModifier = 1;
        _spawning = true;
    }
    void Update()
    {
        if (_spawning && Time.time >= _lastSpawnTime + LevelSpawnDelay * SpawnDelayMultModifier)
        {
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = LevelManager.Instance.GetEnemyToSpawn();
        if (enemy == null) return;
        Vector2 position;
        if (Random.value < 0.5f)
        {
            if (Random.value < 0.5f)
                position = new Vector2(
                    Constants.LEFTBORDER - SPAWNMARGIN,
                    Random.Range(Constants.BOTTOMBORDER, Constants.TOPBORDER));
            else
                position = new Vector2(
                    Constants.RIGHTBORDER + SPAWNMARGIN,
                    Random.Range(Constants.BOTTOMBORDER, Constants.TOPBORDER));
        }
        else
        {
            if (Random.value < 0.5f)
                position = new Vector2(
                    Random.Range(Constants.LEFTBORDER, Constants.RIGHTBORDER),
                    Constants.BOTTOMBORDER - SPAWNMARGIN);
            else
                position = new Vector2(
                    Random.Range(Constants.LEFTBORDER, Constants.RIGHTBORDER),
                    Constants.TOPBORDER + SPAWNMARGIN);
        }
        Instantiate(enemy, position, Quaternion.identity);
        _lastSpawnTime = Time.time;
        OnEnemySpawned.Invoke();
    }
    public void ResetEnemySpawnTimer()
    {
        _lastSpawnTime = Time.time;
    }
}
