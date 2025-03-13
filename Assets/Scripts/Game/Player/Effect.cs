using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Effect
{
    public string EffectName;
    private float _startTime;
    private float _duration;
    public Color Color;

    private Health _playerHealth;
    public int RemainingSeconds
    {
        get
        {
            int seconds = (int)Mathf.Ceil(_duration + _startTime - Time.time);
            // Calling EndEffect when RemainingSeconds is called isn't the best, but it works
            if (seconds <= 0) EndEffect();
            return seconds;
        }
        set
        {
            _duration = value + Time.time - _startTime;
        }
    }
    public Effect(Health playerHealth, string effectType, Color color)
    {
        EffectName = effectType;
        _startTime = Time.time;
        Color = color;
        _playerHealth = playerHealth;
        StartEffect();
    }
    public void Update()
    {
        if (Time.time > _startTime + _duration)
        {
            EndEffect();
        }
    }
    public void StartEffect()
    {
        if (EffectName == "Health") StartHealthEffect();
        else if (EffectName == "Haste") StartHasteEffect();
        else if (EffectName == "Power") StartPowerEffect();
        else if (EffectName == "Luck") StartLuckEffect();
        else if (EffectName == "Stench") StartStenchEffect();
        else if (EffectName == "Poison") StartPoisonEffect();
        else if (EffectName == "Slow") StartSlowEffect();
        else if (EffectName == "Linger") StartLingerEffect();
        EffectDisplayManager.Instance.AddEffectToDisplay(this);
    }
    public void StartHealthEffect()
    {
        _playerHealth.Heal(50);
        _duration = 1;
    }
    public void StartHasteEffect()
    {
        PlayerMovement.Instance.MovementSpeed *= 2;
        _duration = 30;
    }
    public void StartPowerEffect()
    {
        PlayerUseItem.Instance.DamageMultiplier *= 2;
        _duration = 20;
    }
    public void StartLuckEffect()
    {
        LootTableManager.Instance.DropChanceModifier += 0.2f;
        LootTableManager.Instance.ModifierChanceModifier += 0.3f;
        _duration = 60;
    }
    public void StartStenchEffect()
    {
        EnemySpawningManager.Instance.SpawnDelayMultModifier *= 2;
        _duration = 40;
    }
    public void StartPoisonEffect()
    {
        _playerHealth.Damage(30);
        _duration = 1;
    }
    public void StartSlowEffect()
    {
        PlayerMovement.Instance.MovementSpeed *= 0.5f;
        _duration = 15;
    }

    public void StartLingerEffect()
    {
        foreach (Effect effect in EffectDisplayManager.Instance.Effects)
        {
            effect.RemainingSeconds *= 2;
        }
        _duration = 1;
    }
    public void EndEffect()
    {
        if (EffectName == "Haste") EndHasteEffect();
        else if (EffectName == "Power") EndPowerEffect();
        else if (EffectName == "Luck") EndLuckEffect();
        else if (EffectName == "Stench") EndStenchEffect();
        else if (EffectName == "Slow") EndSlowEffect();
        EffectDisplayManager.Instance.RemoveEffect(this);
    }
    public void EndHasteEffect()
    {
        PlayerMovement.Instance.MovementSpeed *= 0.5f;
    }
    public void EndPowerEffect()
    {
        PlayerUseItem.Instance.DamageMultiplier *= 0.5f;
    }
    public void EndLuckEffect()
    {
        LootTableManager.Instance.DropChanceModifier -= 0.2f;
        LootTableManager.Instance.ModifierChanceModifier -= 0.3f;
    }
    public void EndStenchEffect()
    {
        EnemySpawningManager.Instance.SpawnDelayMultModifier *= 0.5f;
    }
    public void EndSlowEffect()
    {
        PlayerMovement.Instance.MovementSpeed *= 2f;
    }
}
