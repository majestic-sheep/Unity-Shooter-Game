using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("References")]
    private Movement _movement;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Settings")]
    [SerializeField] private float _maximumHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private bool _destroyOnDeath;
    public float DamageMultiplier;
    public int InvincibilityFactors;
    public float DestroyDelay;

    private bool _markedDead;

    [SerializeField] private float _staggerUpdateFactor;
    [SerializeField] private float _staggerRequiredForKnockback;
    [SerializeField] private float _knockbackMagnitude;
    [SerializeField] private float _stagger;

    [SerializeField] private float _colorLerpFactor;
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
        _movement = GetComponent<Movement>();

        DamageMultiplier = 1f;
        InvincibilityFactors = 0;
    }
    public void Damage(float damage, Vector2 direction)
    {
        Damage(damage);
        _stagger += damage;
        _movement.Velocity += _knockbackMagnitude * (_stagger - _staggerRequiredForKnockback) * direction;
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
                Destroy(gameObject, DestroyDelay);
            }
        }

        _spriteRenderer.color = Color.red;
    }
    public void Kill()
    {
        _currentHealth = 0;
        if (!_markedDead)
        {
            _markedDead = true;
            OnDied.Invoke();
            if (_destroyOnDeath)
            {
                Destroy(gameObject, DestroyDelay);
            }
        }

        _spriteRenderer.color = Color.red;
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
    private void FixedUpdate()
    {
        _stagger *= _staggerUpdateFactor;
        _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, Color.white, _colorLerpFactor);
    }
}
