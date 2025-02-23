using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _outlineImage;
    private Color _outlineImageUnselectedColor;
    [SerializeField] private Transform _actualImageTransform;
    [SerializeField] private Transform _weaponImageTransform;
    [SerializeField] private Transform _standardImageTransform;
    [SerializeField] private TMP_Text _text;
    public Item Item;
    public bool Active
    {
        set
        {
            if (value)
            {
                _outlineImage.color = Color.white;
                _text.color = Color.white;
                transform.localScale = Vector3.one * _activeScale;
            }
            else
            {
                _outlineImage.color = _outlineImageUnselectedColor;
                _text.color = _outlineImageUnselectedColor;
                transform.localScale = Vector3.one;
            }
        }
    }
    [SerializeField] private float _activeScale;
    public void Awake()
    {
        _outlineImageUnselectedColor = _outlineImage.color;
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
