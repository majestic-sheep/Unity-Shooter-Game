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
    [SerializeField] private PlayerUseItem _playerShoot;
    [SerializeField] private SpriteRenderer _itemSpriteRenderer;
    public List<Item> Items = new();
    public int MaxInventorySize = 9;
    public int CurrentIndex { get; private set; } = 0;
    public Item CurrentItem {
        get
        {
            return Items[CurrentIndex];
        }
    }
    private void Start()
    {
        Items.Add(new Weapon("pistol", "infiAmmo"));
        CurrentIndex = 0;
    }
    public void Update()
    {
        int value = (int)Mouse.current.scroll.ReadValue().normalized.y;
        if (value == 0) return;

        ShiftCurrentWeaponIndexBy(-value);
    }
    public void Add(Item item)
    {
        Items.Add(item);
    }
    public void RemoveCurrentItem()
    {
        Items.RemoveAt(CurrentIndex);
        if (Items.Count == CurrentIndex) { 
            ShiftCurrentWeaponIndexBy(-1);
        }
        else
        {
            UpdateDisplayedWeapon();
        }
    }

    public void UseAmmo()
    {
        Weapon current_weapon = (Weapon)CurrentItem;
        if (current_weapon.InfiAmmo) return;
        current_weapon.Ammo -= 1;
        if (current_weapon.Ammo <= 0) RemoveCurrentItem();
    }
    public void ShiftCurrentWeaponIndexBy(int value)
    {
        CurrentIndex += value;
        CurrentIndex = Mathf.Clamp(CurrentIndex, 0, Items.Count - 1);

        UpdateDisplayedWeapon();
    }
    public void UpdateDisplayedWeapon()
    {
        _itemSpriteRenderer.sprite = CurrentItem.ItemSprite;
        _itemSpriteRenderer.color = CurrentItem.ItemColor;
    }
    private void OnDrop(InputValue inputValue)
    {
        if (CurrentItem is Weapon)
        {
            Weapon current_weapon = (Weapon)CurrentItem;
            if (current_weapon.Modifier == "infiAmmo") return;
        }

        GameObject _collectable = Instantiate(_collectablePrefab, transform.position, Quaternion.identity);
        Transform gunTransform = _itemSpriteRenderer.GetComponentInParent<Transform>();
        _collectable.transform.position = new Vector2(
            _collectable.transform.position.x + gunTransform.lossyScale.x * 10f,
            gunTransform.position.y - 3f);
        _collectable.GetComponent<CollectableBehavior>().SetItem(CurrentItem);

        RemoveCurrentItem();
    }
}
