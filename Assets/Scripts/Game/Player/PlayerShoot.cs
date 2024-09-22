using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _heldGun;
    [SerializeField] private Transform _muzzleTransform;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _fireDelay;
    private bool _firing;
    private float _lastFireTime;
    private void Awake()
    {
        // _lastFireTime should be set to be ready to fire upon awakening
        _lastFireTime = Time.time - _fireDelay;
    }
    void Update()
    {
        if (_firing && Time.time - _lastFireTime >= _fireDelay)
        {
            FireBullet();
            _lastFireTime = Time.time;
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
        rigidbody.velocity = _bulletSpeed * angleToFireIn;
    }
}
