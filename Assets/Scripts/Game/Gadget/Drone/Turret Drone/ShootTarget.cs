using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootTarget : MonoBehaviour
{
    public DeployedTurretDrone DeployedTurretDrone;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Targeter _targeter;
    public Weapon Weapon;
    private float _lastFireTime;
    public bool Firing;
    private int _ammoMax;
    private void Awake()
    {
        _ammoMax = Weapon.Ammo;
    }
    void Update()
    {
        if (Firing && _targeter.HasTarget && Weapon.Ammo > 0 && Time.time - _lastFireTime >= Weapon.FireDelay)
        {
            if (Weapon.BurstDelay > 0)
                StartCoroutine(ShootBurstBullets());
            else
                ShootBullets();
            _lastFireTime = Time.time;
        }
    }
    private void ShootBullets()
    {
        for (int i = 0; i < Weapon.BulletCount; i++)
            FireBullet();
        Weapon.Ammo--;
        if (Weapon.Ammo <= 0)
            StopFiring();
    }
    private IEnumerator ShootBurstBullets()
    {
        for (int i = 0; i < Weapon.BulletCount; i++)
        {
            FireBullet();
            Weapon.Ammo--;
            if (Weapon.Ammo <= 0)
            {
                StopFiring();
                break;
            }
            if (!_targeter.HasTarget)
                break;
            yield return new WaitForSeconds(Weapon.BurstDelay);
        }
    }
    private void FireBullet()
    {
        Vector3 angleToFireIn;
        if (_targeter.TargetLocation == null)
            angleToFireIn = Vector3.right;
        else
            angleToFireIn = (_targeter.TargetLocation - 
                new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).normalized;

        GameObject bullet = Instantiate(_projectilePrefab, gameObject.transform.position, Quaternion.identity);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = Weapon.BulletSpeed * angleToFireIn;
        rigidbody.velocity += new Vector2(
            Random.Range(-Weapon.BulletVelocityMargin, Weapon.BulletVelocityMargin),
            Random.Range(-Weapon.BulletVelocityMargin, Weapon.BulletVelocityMargin));

        bullet.ConvertTo<Bullet>().Damage = Weapon.BulletDamage;
    }
    public void RefillAmmo()
    {
        Weapon.Ammo = _ammoMax;
    }
    public void StartFiring()
    {
        Firing = true;
        _lastFireTime = Time.time;
    }
    private void StopFiring()
    {
        Firing = false;
        DeployedTurretDrone.Deactivate();
    }
}
