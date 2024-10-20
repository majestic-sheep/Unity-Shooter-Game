using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maximumHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private bool _destroyOnDeath;
    public float _destroyDelay;
    public float PercentHealth
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }
    public UnityEvent OnDied;
    public UnityEvent OnHealthChanged;
    public void Damage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maximumHealth);
        OnHealthChanged.Invoke();
        if (_currentHealth == 0)
        {
            OnDied.Invoke();
            if (_destroyOnDeath)
            {
                Destroy(gameObject, _destroyDelay);
            }
        }
    }
    public void HealRatio(float ratio)
    {
        _currentHealth = Mathf.Lerp(_currentHealth, _maximumHealth, ratio);
        OnHealthChanged.Invoke();
    }
}
