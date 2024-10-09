using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableWeapon : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Vector2 _initPos;
    [SerializeField] private float _wobbleYAmplitude;
    [SerializeField] private float _wobbleRotAmplitude;
    [SerializeField] private float _wobbleWavelength;

    private Weapon _weapon;
    void Start()
    {
        _inventory = FindAnyObjectByType<Inventory>();
        _initPos = transform.position;
    }
    void Update()
    {
        float sin = Mathf.Sin(Time.time / _wobbleWavelength);

        transform.position = _initPos + new Vector2(0, Mathf.Abs(_wobbleYAmplitude * sin));
        float angle = sin * _wobbleRotAmplitude;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            if (_weapon == null)
                _inventory.Add(new Weapon("shotgun"));
            else
                _inventory.Add(_weapon);
            Destroy(gameObject);
        }
    }
    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
        _spriteRenderer.sprite = weapon.WeaponSprite;
        _spriteRenderer.color = weapon.WeaponColor;
    }
}
