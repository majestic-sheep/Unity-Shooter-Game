using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;
    private readonly float _offScreenAllowableMargin = 20f;
    [SerializeField] private float _mOfDamageToLocalScale;
    [SerializeField] private float _bOfDamageToLocalScale;
    public float Damage { private get; set; }
    private void Start()
    {
        _camera = Camera.main;
        transform.localScale *= _mOfDamageToLocalScale * Damage + _bOfDamageToLocalScale;
        CameraShake.Instance.ShakeMagnitude += Damage / 10;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyMovement>())
        {
            other.gameObject.GetComponent<Health>().Damage(Damage);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);
        if (screenPosition.x < -_offScreenAllowableMargin ||
            screenPosition.y < -_offScreenAllowableMargin ||
            screenPosition.x > Screen.width + _offScreenAllowableMargin ||
            screenPosition.y > Screen.height + _offScreenAllowableMargin)
            Destroy(gameObject);
    }
}
