using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum WeaponType { Pistol, Shotgun, Rifle, SMG, Custom }
public enum WeaponModifierType { None, Fast, Split, Heavy, InfiAmmo }
[Serializable]
public class Weapon : Item
{
    public WeaponType WeaponType;
    public WeaponModifierType ModifierType;
    public int BulletCount;
    public float BurstDelay;
    public float BulletSpeed;
    public float BulletVelocityMargin;
    public float BulletDamage;
    public float FireDelay;
    public int Ammo;
    public bool InfiAmmo;

    public float LastFireTime;
    /// <summary>
    /// 0-based index of the current bullet in the burst
    /// </summary>
    public int _burstIndex;
    public float CurrentFireDelay
    {
        get
        {
            if (BurstDelay == 0f)
                return FireDelay;
            if (_burstIndex < BulletCount - 1)
                return BurstDelay;
            else
                return FireDelay;
        }
    }
    public Weapon()
    {
        Initialize(WeaponType.Custom, WeaponModifierType.None);
    }
    public Weapon(WeaponType type)
    {
        Initialize(type, WeaponModifierType.None);
    }
    public Weapon(WeaponType type, WeaponModifierType modifier)
    {
        Initialize(type, modifier);
    }
    private void Initialize(WeaponType type, WeaponModifierType modifier)
    {
        SetTypeData(type);
        SetModifierData(modifier);
        _burstIndex = BulletCount - 1;
    }
    private void SetTypeData(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Pistol:
                BulletCount = 1;
                BulletVelocityMargin = 0;
                BulletDamage = 5;
                BulletSpeed = 30;
                FireDelay = 1;
                Ammo = 30;
                ItemSprite = WeaponManager.Instance.PistolSprite;
                ItemColor = Color.white;
                break;
            case WeaponType.Shotgun:
                BulletCount = 5;
                BulletVelocityMargin = 7;
                BulletDamage = 2f;
                BulletSpeed = 25;
                FireDelay = 1.3f;
                Ammo = 15;
                ItemSprite = WeaponManager.Instance.ShotgunSprite;
                ItemColor = Color.white;
                break;
            case WeaponType.Rifle:
                BulletCount = 1;
                BulletVelocityMargin = 0;
                BulletDamage = 2.5f;
                BulletSpeed = 50;
                FireDelay = 0.3125f;
                Ammo = 60;
                ItemSprite = WeaponManager.Instance.RifleSprite;
                ItemColor = Color.white;
                break;
            case WeaponType.SMG:
                BulletCount = 3;
                BurstDelay = 0.05f;
                BulletSpeed = 30;
                BulletVelocityMargin = 0;
                BulletDamage = 2f;
                FireDelay = 0.9375f;
                Ammo = 60;
                ItemSprite = WeaponManager.Instance.SmgSprite;
                ItemColor = Color.white;
                break;
        }
    }
    private void SetModifierData(WeaponModifierType modifier)
    {
        switch (modifier)
        {
            case (WeaponModifierType.Fast):
                FireDelay /= 2;
                BulletVelocityMargin += 1;
                ItemColor = Color.blue;
                break;
            case (WeaponModifierType.Split):
                BulletCount *= 2;
                if (BulletVelocityMargin == 0)
                    BulletVelocityMargin = 5;
                Ammo = 25;
                ItemColor = Color.green;
                break;
            case (WeaponModifierType.Heavy):
                FireDelay *= 1.1f;
                BulletDamage *= 2.2f;
                Ammo /= 2;
                ItemColor = Color.red;
                break;
            case (WeaponModifierType.InfiAmmo):
                BulletDamage *= 0.8f;
                InfiAmmo = true;
                ItemColor = Color.gray;
                break;
        }
    }
    public void Fire(Vector2 origin, Vector3 angle, bool shakeScreen)
    {
        WeaponManager.Instance.FireWeapon(this, origin, angle, shakeScreen);
    }
    public void IncrementBurstIndex()
    {
        _burstIndex++;
        if (_burstIndex >= BulletCount)
            _burstIndex = 0;
    }
    public override bool Equals(Item other)
    {
        if (!base.Equals(other)) return false;

        if (other is not Weapon) return false;
        Weapon weapon = (Weapon)other;

        if (WeaponType != weapon.WeaponType) return false;
        if (ModifierType != weapon.ModifierType) return false;
        if (BulletCount != weapon.BulletCount) return false;
        if (BulletVelocityMargin != weapon.BulletVelocityMargin) return false;
        if (BulletDamage != weapon.BulletDamage) return false;
        if (BulletSpeed != weapon.BulletSpeed) return false;
        if (FireDelay != weapon.FireDelay) return false;
        if (Ammo != weapon.Ammo) return false;
        if (InfiAmmo != weapon.InfiAmmo) return false;
        return true;
    }
    public override Item Clone()
    {
        Weapon weapon = new()
        {
            WeaponType = WeaponType,
            ModifierType = ModifierType,
            BulletCount = BulletCount,
            BulletVelocityMargin = BulletVelocityMargin,
            BulletDamage = BulletDamage,
            BulletSpeed = BulletSpeed,
            FireDelay = FireDelay,
            Ammo = Ammo,
            InfiAmmo = InfiAmmo,
            ItemSprite = ItemSprite,
            ItemColor = ItemColor
        };
        return weapon;
    }
}
