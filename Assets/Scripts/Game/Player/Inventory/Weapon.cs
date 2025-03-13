using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Weapon : Item
{
    public string WeaponType;
    public string Modifier;
    public float BulletCount;
    public float BulletVelocityMargin;
    public float BulletDamage;
    public float BulletSpeed;
    public float FireDelay;
    public int Ammo;
    public bool InfiAmmo;
    public Weapon()
    {
        WeaponType = null;
        Modifier = null;
    }
    public Weapon(string type)
    {
        if (SetTypeData(type)) WeaponType = type;
        else WeaponType = null;
        Modifier = null;
    }
    public Weapon(string type, string modifier)
    {
        if (SetTypeData(type)) WeaponType = type;
        else WeaponType = null;
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
            ItemSprite = Inventory.Instance.PistolSprite;
            ItemColor = Color.white;
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
            ItemSprite = Inventory.Instance.RifleSprite;
            ItemColor = Color.white;
            return true;
        }
        else if (type.Equals("shotgun"))
        {
            BulletCount = 5;
            BulletVelocityMargin = 7;
            BulletDamage = 2f;
            BulletSpeed = 25;
            FireDelay = 1.3f;
            Ammo = 15;
            ItemSprite = Inventory.Instance.ShotgunSprite;
            ItemColor = Color.white;
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
            ItemColor = Color.blue;
            return true;
        }
        else if (modifier.Equals("split"))
        {
            FireDelay *= 1.1f;
            BulletDamage *= 0.9f;
            BulletCount *= 2;
            if (BulletVelocityMargin == 0) BulletVelocityMargin = 5;
            Ammo = 25;
            ItemColor = Color.green;
            return true;
        }
        else if (modifier.Equals("heavy"))
        {
            FireDelay *= 1.1f;
            BulletDamage *= 2.2f;
            Ammo /= 2;
            ItemColor = Color.red;
            return true;
        }
        else if (modifier.Equals("infiAmmo"))
        {
            BulletDamage *= 0.8f;
            InfiAmmo = true;
            ItemColor = Color.gray;
            return true;
        }
        return false;
    }
    public override bool Equals(Item other)
    {
        if (!base.Equals(other)) return false;

        if (other is not Weapon) return false;
        Weapon weapon = (Weapon)other;

        if (WeaponType != weapon.WeaponType) return false;
        if (Modifier != weapon.Modifier) return false;
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
            Modifier = Modifier,
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
