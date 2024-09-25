using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image _healthBarForeground;
    [SerializeField] private float _targetPercent;
    [SerializeField] private float _displayedPercent;
    [SerializeField] private float _lerpRate;
    public void UpdateHealthBar(Health health)
    {
        _targetPercent = health.PercentHealth;
    }
    public void FixedUpdate()
    {
        _healthBarForeground.fillAmount = _displayedPercent;
        _healthBarForeground.color = Color.Lerp(Color.red, Color.green, _displayedPercent);
        _displayedPercent = Mathf.Lerp(_displayedPercent, _targetPercent, _lerpRate);
    }
}
