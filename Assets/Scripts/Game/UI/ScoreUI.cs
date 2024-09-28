using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private GameProgress _gameProgress;
    public void UpdateScore()
    {
        gameObject.GetComponent<TMP_Text>().text = $"Score: {_gameProgress.Score}";
    }
}
