using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoScript
{
    private Inventory _inventory;

    public string Type;
    public string Modifier;
    public float BulletCount;
    public float BulletVelocityMargin;
    public float BulletDamage;
    public float BulletSpeed;
    public float FireDelay;
    public int Ammo;
    public bool InfiAmmo;
    public Sprite WeaponSprite;
    public Color WeaponColor;
    public Weapon()
    {
        _inventory = FindAnyObjectByType<Inventory>();
        Type = null;
        Modifier = null;
    }
    public Weapon(string type)
    {
        _inventory = FindAnyObjectByType<Inventory>();

        if (SetTypeData(type)) Type = type;
        else Type = null;
        Modifier = null;
    }
    public Weapon(string type, string modifier)
    {
        _inventory = FindAnyObjectByType<Inventory>();

        if (SetTypeData(type)) Type = type;
        else Type = null;
        if (SetModifierData(modifier)) Modifier = modifier;
        else Modifier = null;
    }
    private bool SetTypeData(string type)
    {
        if (type.Equals("pistol"))
        {
            BulletCount = 1;
            BulletVelocityMargin = 0;
            BulletDamage = 5;
            BulletSpeed = 30;
            FireDelay = 1;
            Ammo = 30;
            InfiAmmo = true;
            WeaponSprite = _inventory.PistolSprite;
            WeaponColor = Color.white;
            return true;
        }
        else if (type.Equals("rifle"))
        {
            BulletCount = 1;
            BulletVelocityMargin = 0;
            BulletDamage = 2.5f;
            BulletSpeed = 50;
            FireDelay = 0.3125f;
            Ammo = 60;
            InfiAmmo = false;
            WeaponSprite = _inventory.RifleSprite;
            WeaponColor = Color.white;
            return true;
        }
        else if (type.Equals("shotgun"))
        {
            BulletCount = 5;
            BulletVelocityMargin = 7;
            BulletDamage = 1.76f;
            BulletSpeed = 25;
            FireDelay = 1.3f;
            Ammo = 15;
            InfiAmmo = false;
            WeaponSprite = _inventory.ShotgunSprite;
            WeaponColor = Color.white;
            return true;
        }
        return false;
    }
    private bool SetModifierData(string modifier)
    {
        if (modifier.Equals("fast"))
        {
            FireDelay /= 2;
            BulletVelocityMargin += 1;
            Ammo = Ammo * 5 / 6;
            InfiAmmo = false;
            WeaponColor = Color.blue;
            return true;
        }
        else if (modifier.Equals("split"))
        {
            FireDelay *= 1.1f;
            BulletDamage *= 0.9f;
            BulletCount *= 2;
            if (BulletVelocityMargin == 0) BulletVelocityMargin = 5;
            Ammo = 25;
            InfiAmmo = false;
            WeaponColor = Color.green;
            return true;
        }
        else if (modifier.Equals("heavy"))
        {
            FireDelay *= 1.1f;
            BulletDamage *= 2;
            Ammo = 10;
            InfiAmmo = false;
            WeaponColor = Color.red;
            return true;
        }
        return false;
    }
    public bool Equals(Weapon weapon)
    {
        if (weapon == null) return false;
        if (Type != weapon.Type) return false;
        if (Modifier != weapon.Modifier) return false;
        if (BulletCount != weapon.BulletCount) return false;
        if (BulletVelocityMargin != weapon.BulletVelocityMargin) return false;
        if (BulletDamage != weapon.BulletDamage) return false;
        if (BulletSpeed != weapon.BulletSpeed) return false;
        if (FireDelay != weapon.FireDelay) return false;
        if (Ammo != weapon.Ammo) return false;
        if (InfiAmmo != weapon.InfiAmmo) return false;
        if (WeaponSprite != weapon.WeaponSprite) return false;
        if (WeaponColor != weapon.WeaponColor) return false;
        return true;
    }
    public Weapon Clone()
    {
        Weapon weapon = new();
        weapon.Type = Type;
        weapon.Modifier = Modifier;
        weapon.BulletCount = BulletCount;
        weapon.BulletVelocityMargin = BulletVelocityMargin;
        weapon.BulletDamage = BulletDamage;
        weapon.BulletSpeed = BulletSpeed;
        weapon.FireDelay = FireDelay;
        weapon.Ammo = Ammo;
        weapon.InfiAmmo = InfiAmmo;
        weapon.WeaponSprite = WeaponSprite;
        weapon.WeaponColor = WeaponColor;
        return weapon;
    }
}
