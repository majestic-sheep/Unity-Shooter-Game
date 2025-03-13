using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplayUI : MonoBehaviour
{
    public void UpdateLevelName()
    {
        gameObject.GetComponent<TMP_Text>().text = LevelManager.Instance.DisplayedLevelName;
    }
}
