using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private GameObject _projectilePrefab;
    public Sprite PistolSprite;
    public Sprite RifleSprite;
    public Sprite ShotgunSprite;
    public Sprite SmgSprite;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void FireWeapon(Weapon weapon, Vector2 originPosition, Vector3 angle, bool shakeScreen)
    {
        float totalDamage = weapon.BulletDamage;
        if (weapon.BulletCount == 1)
        {
            FireBullet(weapon, originPosition, angle);
        }
        else if (weapon.BurstDelay > 0)
        {
            FireBulletInBurst(weapon, originPosition, angle);
        }
        else
        {
            FireBulletCluster(weapon, originPosition, angle);
            totalDamage *= weapon.BulletCount;
        }
        weapon.LastFireTime = Time.time;
        weapon.Ammo--;

        if (shakeScreen)
        {
            ShakeScreen(totalDamage);
        }
    }
    private void FireBulletInBurst(Weapon weapon, Vector2 originPosition, Vector3 angle)
    {
        FireBullet(weapon, originPosition, angle);
        weapon.IncrementBurstIndex();
    }
    private void FireBulletCluster(Weapon weapon, Vector2 originPosition, Vector3 angle)
    {
        for (int i = 0; i < weapon.BulletCount; i++)
            FireBullet(weapon, originPosition, angle);
    }
    private void FireBullet(Weapon weapon, Vector2 originPosition, Vector3 angle)
    {
        GameObject bullet = Instantiate(_projectilePrefab, originPosition, Quaternion.identity);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = weapon.BulletSpeed * angle;
        rigidbody.velocity += new Vector2(
            Random.Range(-weapon.BulletVelocityMargin, weapon.BulletVelocityMargin),
            Random.Range(-weapon.BulletVelocityMargin, weapon.BulletVelocityMargin));

        bullet.ConvertTo<Bullet>().Damage = weapon.BulletDamage * PlayerUseItem.Instance.DamageMultiplier;
    }
    private void ShakeScreen(float totalDamage)
    {
        CameraShake.Instance.ShakeMagnitude += totalDamage / 10;
    }
}
