using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningManager : MonoBehaviour
{

    [SerializeField] private GameObject _enemy1Prefab;
    [SerializeField] private GameObject _enemy2Prefab;
    [SerializeField] private GameObject _enemy3Prefab;
    [SerializeField] private GameObject _enemy4Prefab;
    [SerializeField] private float _spawnDelay;
    private float _lastSpawnTime;

    private const float SPAWNMARGIN = 15f;
    // Start is called before the first frame update
    void Awake()
    {
        _lastSpawnTime = Time.time - _spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _lastSpawnTime + _spawnDelay)
        {
            Spawn();
        }
    }
    void Spawn()
    {
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
        GameObject enemy = ChooseEnemy();
        Instantiate(enemy, position, Quaternion.identity);
        _lastSpawnTime = Time.time;
    }
    private GameObject ChooseEnemy()
    {
        float random = Random.value;
        if (random < 0.4) return _enemy3Prefab;
        if (random < 0.7) return _enemy2Prefab;
        if (random < 0.9) return _enemy1Prefab;
        return _enemy4Prefab;
    }
}
