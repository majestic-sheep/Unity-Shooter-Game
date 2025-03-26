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
    public bool Firing;
    private int _ammoMax;
    private void Awake()
    {
        _ammoMax = Weapon.Ammo;
    }
    void Update()
    {
        if (Firing && _targeter.HasTarget && Weapon.Ammo > 0 && Time.time - Weapon.LastFireTime >= Weapon.CurrentFireDelay)
        {
            Vector3 angleToFireIn;
            if (_targeter.TargetLocation == null)
                angleToFireIn = Vector3.right;
            else
                angleToFireIn = (_targeter.TargetLocation -
                    new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).normalized;

            Weapon.Fire(gameObject.transform.position, angleToFireIn, false);

            if (Weapon.Ammo <= 0)
                StopFiring();
        }
    }
    public void RefillAmmo()
    {
        Weapon.Ammo = _ammoMax;
    }
    public void StartFiring()
    {
        Firing = true;
        Weapon.LastFireTime = Time.time;
    }
    private void StopFiring()
    {
        Firing = false;
        DeployedTurretDrone.Deactivate();
    }
}
