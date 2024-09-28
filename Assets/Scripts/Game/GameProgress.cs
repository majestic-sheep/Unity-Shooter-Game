using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameProgress : MonoBehaviour
{
    public UnityEvent OnScoreChanged;
    public int Score { get; private set; }
    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged.Invoke();
    }
}
