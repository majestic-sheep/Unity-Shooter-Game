using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScoreAllocator : MonoBehaviour
{
    [SerializeField] private int _killScore;
    private GameProgress _gameProgress;

    private void Awake()
    {
        _gameProgress = FindObjectOfType<GameProgress>();
    }
    public void AddPoints()
    {
        _gameProgress.AddScore(_killScore);
    }
}
