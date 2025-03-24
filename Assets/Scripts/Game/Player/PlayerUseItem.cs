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
    private float _lastFireTime = -1000f;
    private Weapon CurrentWeapon
    {
        get
        {
            if (Inventory.Instance.CurrentItem is Weapon weapon)
            {
                return weapon;
            }
            return null;
        }
    }
    private Potion CurrentPotion
    {
        get
        {
            if (Inventory.Instance.CurrentItem is Potion potion)
            {
                return potion;
            }
            return null;
        }
    }
    private Gadget CurrentGadget
    {
        get
        {
            if (Inventory.Instance.CurrentItem is Gadget gadget)
            {
                return gadget;
            }
            return null;
        }
    }
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
        if (CurrentWeapon == null) return;

        if (_firing && Time.time - _lastFireTime >= CurrentWeapon.FireDelay)
        {
            for (int i = 0; i < CurrentWeapon.BulletCount; i++)
                FireBullet();
            _lastFireTime = Time.time;

            Inventory.Instance.UseAmmo();
        }
    }
    public void OnUseItem(InputValue inputValue)
    {
        if (inputValue.isPressed && !ScreenClickManager.Instance.MouseIsOverScreen)
            return;
        if (inputValue.isPressed)
        {
            if (CurrentWeapon != null)
            {
                _firing = true;
            }
        }
        else
        {
            _firing = false;
            return;
        }
        if (CurrentPotion != null)
        {
            PotionManager.Instance.ExecutePotionEffect(CurrentPotion);
            Inventory.Instance.RemoveCurrentItem();
        }
        else if (CurrentGadget != null)
        {
            CurrentGadget.Deploy();
            Inventory.Instance.RemoveCurrentItem();
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
