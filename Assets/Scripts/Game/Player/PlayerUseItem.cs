using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseItem : MonoBehaviour
{
    public static PlayerUseItem Instance { get; private set; }

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _heldGun;
    [SerializeField] private Transform _muzzleTransform;
    public float DamageMultiplier;

    private bool _firing;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Update()
    {
        if (Inventory.Instance.CurrentItem is Weapon weapon)
        {
            if (_firing && Time.time - weapon.LastFireTime >= weapon.CurrentFireDelay)
            {
                Vector3 angleToFireIn;
                if (_heldGun.transform.lossyScale.x > 0)
                    angleToFireIn = _heldGun.transform.right;
                else
                    angleToFireIn = (Vector3.zero - _heldGun.transform.right);

                weapon.Fire(_muzzleTransform.position, angleToFireIn, true);

                Inventory.Instance.CheckIfWeaponConsumed();
            }
        }
    }
    public void OnUseItem(InputValue inputValue)
    {
        if (inputValue.isPressed && !ScreenClickManager.Instance.MouseIsOverScreen)
            return;
        if (inputValue.isPressed)
        {
            if (Inventory.Instance.CurrentItem is Weapon)
            {
                _firing = true;
            }
        }
        else
        {
            _firing = false;
            return;
        }
        if (Inventory.Instance.CurrentItem is Potion potion)
        {
            PotionManager.Instance.ExecutePotionEffect(potion);
            Inventory.Instance.RemoveCurrentItem();
        }
        else if (Inventory.Instance.CurrentItem is Gadget gadget)
        {
            gadget.Deploy();
            Inventory.Instance.RemoveCurrentItem();
        }
    }
}
