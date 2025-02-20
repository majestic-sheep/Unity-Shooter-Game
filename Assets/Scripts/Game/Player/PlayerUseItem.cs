using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseItem : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _heldGun;
    [SerializeField] private Transform _muzzleTransform;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PotionManager _potionManager;
    public float DamageMultiplier;

    private bool _firing;
    private float _lastFireTime = -1000f;
    private Weapon CurrentWeapon
    {
        get
        {
            if (_inventory.CurrentItem is Weapon)
            {
                return (Weapon)_inventory.CurrentItem;
            }
            return null;
        }
    }
    private Potion CurrentPotion
    {
        get
        {
            if (_inventory.CurrentItem is Potion)
            {
                return (Potion)_inventory.CurrentItem;
            }
            return null;
        }
    }
    void Update()
    {
        if (CurrentWeapon == null) return;

        if (_firing && Time.time - _lastFireTime >= CurrentWeapon.FireDelay)
        {
            for (int i = 0; i < CurrentWeapon.BulletCount; i++)
                FireBullet();
            _lastFireTime = Time.time;

            _inventory.UseAmmo();
        }
    }
    private void OnUseItem(InputValue inputValue)
    {
        _firing = inputValue.isPressed && CurrentWeapon != null;
        if (inputValue.isPressed && CurrentPotion != null)
        {
            _potionManager.ExecutePotionEffect(CurrentPotion);
            _inventory.RemoveCurrentItem();
        }
    }
    private void FireBullet()
    {
        Vector3 angleToFireIn;
        if (_heldGun.transform.lossyScale.x > 0)
            angleToFireIn = _heldGun.transform.right;
        else
            angleToFireIn = (Vector3.zero - _heldGun.transform.right);

        GameObject bullet = Instantiate(_bulletPrefab, _muzzleTransform.position, Quaternion.identity);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = CurrentWeapon.BulletSpeed * angleToFireIn;
        rigidbody.velocity += new Vector2(
            Random.Range(
                -CurrentWeapon.BulletVelocityMargin,
                CurrentWeapon.BulletVelocityMargin),
            Random.Range(
                -CurrentWeapon.BulletVelocityMargin,
                CurrentWeapon.BulletVelocityMargin));

        bullet.ConvertTo<Bullet>().Damage = CurrentWeapon.BulletDamage * DamageMultiplier;
    }
}
