using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] private GameObject _collectablePrefab;

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
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Add(new Weapon(WeaponType.Pistol, WeaponModifierType.InfiAmmo));
        CurrentIndex = 0;
    }
    public void Update()
    {
        if (!PauseManager.Instance.Paused)
        {
            ShiftOnMouseScroll();
        }
    }
    private void ShiftOnMouseScroll()
    {
        int value = (int)Mouse.current.scroll.ReadValue().normalized.y;
        if (value == 0) return;

        ShiftCurrentWeaponIndexBy(-value);
    }
    public void Add(Item item)
    {
        if (Items.Count >= MaxInventorySize)
            CreateCollectable(item);
        else
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
            UpdateDisplayedItem();
        }
        if (CurrentItem is Weapon weapon)
        {
            weapon.LastFireTime = Time.time;
        }
    }

    public void CheckIfWeaponConsumed()
    {
        Weapon current_weapon = (Weapon)CurrentItem;
        if (current_weapon.InfiAmmo) return;
        if (current_weapon.Ammo <= 0) RemoveCurrentItem();
    }
    public void ShiftCurrentWeaponIndexBy(int value)
    {
        CurrentIndex += value;
        CurrentIndex = Mathf.Clamp(CurrentIndex, 0, Items.Count - 1);

        UpdateDisplayedItem();
    }
    public void UpdateDisplayedItem()
    {
        _itemSpriteRenderer.sprite = CurrentItem.ItemSprite;
        _itemSpriteRenderer.color = CurrentItem.ItemColor;
    }
    private void OnDrop(InputValue inputValue)
    {
        if (!PauseManager.Instance.Paused)
        {
            DropCurrentItem();
        }
    }
    private void DropCurrentItem()
    {
        if (CurrentItem is Weapon current_weapon)
        {
            if (current_weapon.ModifierType == WeaponModifierType.InfiAmmo) return;
        }
        CreateCollectable(CurrentItem);
        RemoveCurrentItem();
    }
    private void CreateCollectable(Item item)
    {
        GameObject _collectable = Instantiate(_collectablePrefab, transform.position, Quaternion.identity);
        Transform itemSpriteRendererTransform = _itemSpriteRenderer.GetComponentInParent<Transform>();
        _collectable.transform.position = new Vector2(
            _collectable.transform.position.x + itemSpriteRendererTransform.lossyScale.x * 10f,
            itemSpriteRendererTransform.position.y - 3f);
        _collectable.GetComponent<CollectableBehavior>().SetItem(item);
    }
}
