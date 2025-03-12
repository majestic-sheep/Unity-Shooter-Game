using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public void UpdateScore()
    {
        gameObject.GetComponent<TMP_Text>().text = $"Score: {GameProgress.Instance.Score}";
    }
}
