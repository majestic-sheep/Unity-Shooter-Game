using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawningManager : MonoBehaviour
{

    [SerializeField] private LevelManager _levelManager;
    public float SpawnDelay;
    private float _lastSpawnTime;

    private const float SPAWNMARGIN = 15f;

    public UnityEvent OnEnemySpawned;
    void Awake()
    {
        _lastSpawnTime = Time.time - SpawnDelay;
    }
    void Update()
    {
        if (Time.time >= _lastSpawnTime + SpawnDelay)
        {
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = _levelManager.GetEnemyToSpawn();
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
}
