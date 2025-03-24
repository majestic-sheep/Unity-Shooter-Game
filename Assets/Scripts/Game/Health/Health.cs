using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maximumHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private bool _destroyOnDeath;
    public float DamageMultiplier;
    public int InvincibilityFactors;
    public float _destroyDelay;
    private bool _markedDead;
    public float PercentHealth
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }
    public UnityEvent OnDied;
    public UnityEvent OnHealthChanged;
    public UnityEvent<float> OnDamageBlocked;
    private void Awake()
    {
        DamageMultiplier = 1f;
        InvincibilityFactors = 0;
    }
    public void Damage(float damage)
    {
        if (InvincibilityFactors > 0)
        {
            OnDamageBlocked.Invoke(damage);
            return;
        }

        float effectiveDamage = damage * DamageMultiplier;
        _currentHealth = Mathf.Clamp(_currentHealth - effectiveDamage, 0, _maximumHealth);
        OnHealthChanged.Invoke();
        if (_currentHealth == 0 && !_markedDead)
        {
            _markedDead = true;
            OnDied.Invoke();
            if (_destroyOnDeath)
            {
                Destroy(gameObject, _destroyDelay);
            }
        }
    }
    public void Heal(float heal)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + heal, 0, _maximumHealth);
        OnHealthChanged.Invoke();
    }
    public void HealRatio(float ratio)
    {
        _currentHealth = Mathf.Lerp(_currentHealth, _maximumHealth, ratio);
        OnHealthChanged.Invoke();
    }
}
