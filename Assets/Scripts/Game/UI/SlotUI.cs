using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Transform _actualImageTransform;
    [SerializeField] private Transform _weaponImageTransform;
    [SerializeField] private Transform _standardImageTransform;
    [SerializeField] private TMP_Text _text;
    public Item Item;
    public bool Active;
    [SerializeField] private float _activeScale;
    public void Update()
    {
        if (Active) transform.localScale = Vector3.one * _activeScale;
        else transform.localScale = Vector3.one;
    }
    public void SetItem(Item item)
    {
        Item = item;
        _image.sprite = item.ItemSprite;
        _image.color = item.ItemColor;
        if (item is Weapon weapon)
        {
            SetDisplayedAmmo(weapon);
            _actualImageTransform.position = _weaponImageTransform.position;
            _actualImageTransform.rotation = _weaponImageTransform.rotation;
            _actualImageTransform.localScale = _weaponImageTransform.localScale;
        }
        else
        {
            _text.text = "";
            _actualImageTransform.position = _standardImageTransform.position;
            _actualImageTransform.rotation = _standardImageTransform.rotation;
            _actualImageTransform.localScale = _standardImageTransform.localScale;
        }
    }
    public void SetDisplayedAmmo(Weapon weapon)
    {
        if (!weapon.InfiAmmo) _text.text = $"{weapon.Ammo}";
        else _text.text = "∞";
    }
}
