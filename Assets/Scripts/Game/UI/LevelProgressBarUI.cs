using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image _levelProgressBarForeground;

    [SerializeField] private float _displayedProgress;
    private float _targetProgress;
    [SerializeField] private float _progressAcceleration;
    public void Update()
    {
        if (LevelManager.Instance.IsIntermission) {
            _displayedProgress = LevelManager.Instance.LevelProgress;
        }
    }
    public void FixedUpdate()
    {
        _displayedProgress = Mathf.Lerp(_displayedProgress, _targetProgress, _progressAcceleration);
        _levelProgressBarForeground.fillAmount = _displayedProgress;
    }
    public void UpdateBar()
    {
        _targetProgress = LevelManager.Instance.LevelProgress;
    }
}
