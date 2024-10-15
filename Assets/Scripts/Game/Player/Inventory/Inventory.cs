using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public Sprite PistolSprite;
    public Sprite RifleSprite;
    public Sprite ShotgunSprite;
    [SerializeField] private PlayerShoot _playerShoot;
    [SerializeField] private SpriteRenderer _gunSpriteRenderer;
    public List<Weapon> Weapons = new();
    public int CurrentWeaponIndex { get; private set; } = 0;
    public Weapon CurrentWeapon {
        get
        {
            return Weapons[CurrentWeaponIndex];
        }
    }
    private void Start()
    {
        Weapons.Add(new Weapon("pistol", "infiAmmo"));
        CurrentWeaponIndex = 0;
    }
    public void Update()
    {
        int value = (int)Mouse.current.scroll.ReadValue().normalized.y;
        if (value == 0) return;

        ShiftCurrentWeaponIndexBy(-value);
    }
    public void Add(Weapon weapon)
    {
        Weapons.Add(weapon);
    }
    public void UseAmmo()
    {
        if (CurrentWeapon.InfiAmmo) return;
        CurrentWeapon.Ammo -= 1;
        if (CurrentWeapon.Ammo <= 0)
        {
            Weapons.RemoveAt(CurrentWeaponIndex);
            ShiftCurrentWeaponIndexBy(-1);
        }
    }
    public void ShiftCurrentWeaponIndexBy(int value)
    {
        CurrentWeaponIndex += value;
        CurrentWeaponIndex = Mathf.Clamp(CurrentWeaponIndex, 0, Weapons.Count - 1);

        _gunSpriteRenderer.sprite = CurrentWeapon.WeaponSprite;
        _gunSpriteRenderer.color = CurrentWeapon.WeaponColor;
    }
}
