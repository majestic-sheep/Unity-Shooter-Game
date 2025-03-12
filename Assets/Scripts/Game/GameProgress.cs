using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance;
    public UnityEvent OnScoreChanged;
    public int Score { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged.Invoke();
    }
}
