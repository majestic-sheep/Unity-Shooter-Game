using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScoreAllocator : MonoBehaviour
{
    [SerializeField] private int _killScore;
    public void AddPoints()
    {
        GameProgress.Instance.AddScore(_killScore);
    }
}
