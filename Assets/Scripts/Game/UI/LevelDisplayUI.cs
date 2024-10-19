using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplayUI : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    public void UpdateLevel()
    {
        gameObject.GetComponent<TMP_Text>().text = $"Level: {_levelManager.Level}";
    }
}
