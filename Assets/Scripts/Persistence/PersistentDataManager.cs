using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

public class PersistentDataManager : MonoBehaviour
{
    public static PersistentDataManager Instance;
    public RunStatsCollection BestRun;
    private string _progressPath;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        _progressPath = $"{Application.persistentDataPath}/progress.txt";
        LoadProgress();
    }
    public void CollectLeaderstats()
    {
        if (BestRun == null || GameProgress.Instance.Score > BestRun.Score)
        {
            BestRun = new()
            {
                Score = GameProgress.Instance.Score,
                DateTime = DateTime.Now,
                RunTime = Time.timeSinceLevelLoad,
                Level = LevelManager.Instance.Level
            };
            SaveProgress();
        }
    }
    private void LoadProgress()
    {
        if (!File.Exists(_progressPath))
            File.WriteAllText(_progressPath, "");

        StreamReader reader = new(_progressPath);
        string line = reader.ReadLine();
        if (line != null)
        {
            BestRun = new();
            JsonUtility.FromJsonOverwrite(line, BestRun);
        }
        reader.Close();
    }
    private void SaveProgress()
    {
        StreamWriter writer = new(_progressPath, false);
        string json = JsonUtility.ToJson(BestRun);
        writer.Write(json);
        writer.Close();
    }
}