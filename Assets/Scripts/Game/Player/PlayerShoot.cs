using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _heldGun;
    [SerializeField] private Transform _muzzleTransform;
    [SerializeField] private Inventory _inventory;

    private bool _firing;
    private float _lastFireTime = -1000f;
    void Update()
    {
        if (_firing && Time.time - _lastFireTime >= _inventory.CurrentWeapon.FireDelay)
        {
            for (int i = 0; i < _inventory.CurrentWeapon.BulletCount; i++)
                FireBullet();
            _lastFireTime = Time.time;

            _inventory.UseAmmo();
        }
    }
    private void OnFire(InputValue inputValue)
    {
        _firing = inputValue.isPressed;
    }
    private void FireBullet()
    {
        /* 
         * This function has a bug. If the player fires during the frame of an x-axis scale flip, it will fire as 
         * though the gun were on the opposite side.
         */
        Vector3 angleToFireIn;
        if (_heldGun.transform.lossyScale.x > 0)
            angleToFireIn = _heldGun.transform.right;
        else
            angleToFireIn = (Vector3.zero - _heldGun.transform.right);

        GameObject bullet = Instantiate(_bulletPrefab, _muzzleTransform.position, Quaternion.identity);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _inventory.CurrentWeapon.BulletSpeed * angleToFireIn;
        rigidbody.velocity += new Vector2(
            Random.Range(
                -_inventory.CurrentWeapon.BulletVelocityMargin, 
                _inventory.CurrentWeapon.BulletVelocityMargin),
            Random.Range(
                -_inventory.CurrentWeapon.BulletVelocityMargin, 
                _inventory.CurrentWeapon.BulletVelocityMargin));

        bullet.ConvertTo<Bullet>().Damage = _inventory.CurrentWeapon.BulletDamage;
    }
}
