using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _collectablePrefab;

    public Sprite PistolSprite;
    public Sprite RifleSprite;
    public Sprite ShotgunSprite;
    [SerializeField] private PlayerShoot _playerShoot;
    [SerializeField] private SpriteRenderer _gunSpriteRenderer;
    public List<Weapon> Weapons = new();
    public int WeaponMaxCount = 9;
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
    private void RemoveCurrentWeapon()
    {
        Weapons.RemoveAt(CurrentWeaponIndex);
        if (Weapons.Count == CurrentWeaponIndex) { 
            ShiftCurrentWeaponIndexBy(-1);
        }
        else
        {
            UpdateDisplayedWeapon();
        }
    }

    public void UseAmmo()
    {
        if (CurrentWeapon.InfiAmmo) return;
        CurrentWeapon.Ammo -= 1;
        if (CurrentWeapon.Ammo <= 0) RemoveCurrentWeapon();
    }
    public void ShiftCurrentWeaponIndexBy(int value)
    {
        CurrentWeaponIndex += value;
        CurrentWeaponIndex = Mathf.Clamp(CurrentWeaponIndex, 0, Weapons.Count - 1);

        UpdateDisplayedWeapon();
    }
    public void UpdateDisplayedWeapon()
    {
        _gunSpriteRenderer.sprite = CurrentWeapon.WeaponSprite;
        _gunSpriteRenderer.color = CurrentWeapon.WeaponColor;
    }
    private void OnDrop(InputValue inputValue)
    {
        if (CurrentWeapon.Modifier == "infiAmmo") return;

        GameObject _collectable = Instantiate(_collectablePrefab, transform.position, Quaternion.identity);
        Transform gunTransform = _gunSpriteRenderer.GetComponentInParent<Transform>();
        _collectable.transform.position = new Vector2(
            _collectable.transform.position.x + gunTransform.lossyScale.x * 10f,
            gunTransform.position.y - 3f);
        _collectable.GetComponent<CollectableWeapon>().SetWeapon(CurrentWeapon);

        RemoveCurrentWeapon();
    }
}
