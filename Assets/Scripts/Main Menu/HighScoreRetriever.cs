using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HighScoreRetriever : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    void Start()
    {
        RunStatsCollection run = PersistentDataManager.Instance.BestRun;
        if (run != null)
        {
            _text.text = $"{run.DateTimeString}\n" +
                $"Scored {run.Score}\n" +
                $"Achieved Level {run.Level}\n" +
                $"Survived for {run.RunTimeString}";
        }
        else
        {
            _text.text = $"None";
        }
    }
}
