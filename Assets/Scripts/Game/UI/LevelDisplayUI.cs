using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplayUI : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    public void UpdateLevelName()
    {
        gameObject.GetComponent<TMP_Text>().text = _levelManager.DisplayedLevelName;
    }
}
