using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;
    private readonly float _offScreenAllowableMargin = 20f;
    [SerializeField] private float _mOfDamageToLocalScale;
    [SerializeField] private float _bOfDamageToLocalScale;
    [SerializeField] private float _minLocalScale;
    [SerializeField] private float _maxLocalScale;
    [SerializeField] private Color _colorAtLowDamage;
    [SerializeField] private Color _colorAtHighDamage;
    public float Damage { private get; set; }
    public string TargetTag;
    private void Start()
    {
        transform.localScale *= _mOfDamageToLocalScale * Damage + _bOfDamageToLocalScale;
        if (transform.localScale.x <= _minLocalScale)
        {
            GetComponent<SpriteRenderer>().color = _colorAtLowDamage;
            transform.localScale = new Vector3(_minLocalScale, _minLocalScale, 1f);
        }
        if (transform.localScale.x >= _maxLocalScale)
        {
            GetComponent<SpriteRenderer>().color = _colorAtHighDamage;
            transform.localScale = new Vector3(_maxLocalScale, _maxLocalScale, 1f);
        }
        _camera = Camera.main;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyMovement>())
        {
            other.gameObject.GetComponent<Health>().Damage(Damage, GetComponent<Rigidbody2D>().velocity.normalized);
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
