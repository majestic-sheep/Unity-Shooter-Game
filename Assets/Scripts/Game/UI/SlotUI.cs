using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    public Weapon Weapon;
    public bool Active;
    [SerializeField] private float _activeScale;
    public void Update()
    {
        if (Active) transform.localScale = Vector3.one * _activeScale;
        else transform.localScale = Vector3.one;
    }
    public void SetWeapon(Weapon weapon)
    {
        Weapon = weapon;
        _image.sprite = weapon.WeaponSprite;
        _image.color = weapon.WeaponColor;
        if (!weapon.InfiAmmo) _text.text = $"{weapon.Ammo}";
        else _text.text = "∞";
    }
}
