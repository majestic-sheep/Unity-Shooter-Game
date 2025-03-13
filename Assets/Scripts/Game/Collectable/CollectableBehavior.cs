using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehavior : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Vector2 _initPos;
    private float _initTime;
    [SerializeField] private float _wobbleYAmplitude;
    [SerializeField] private float _wobbleRotAmplitude;
    [SerializeField] private float _wobbleWavelength;
    [SerializeField] private float _fadeAfter;
    [SerializeField] private float _deleteAfter;
    [SerializeField] private float _fadeAmplitude;
    [SerializeField] private float _fadeWavelength;

    private Item _item;
    void Start()
    {
        _initPos = transform.position;
        _initTime = Time.time;
    }
    void Update()
    {
        Wobble();
        if (Time.time - _initTime >= _fadeAfter) Fade();
        if (Time.time - _initTime >= _deleteAfter) Destroy(gameObject);
    }
    private void Wobble()
    {
        float wave = Mathf.Sin(Time.time / _wobbleWavelength);

        transform.position = _initPos + new Vector2(0, Mathf.Abs(_wobbleYAmplitude * wave));
        float angle = wave * _wobbleRotAmplitude;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void Fade()
    {
        float fadeTime = Time.time - _fadeAfter;
        float wave = 1 - ((Mathf.Cos(fadeTime / _fadeWavelength) + 1) / 2);

        _spriteRenderer.color = Color.Lerp(
            _item.ItemColor,
            Color.clear,
            wave * _fadeAmplitude);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (Inventory.Instance.Items.Count >= Inventory.Instance.MaxInventorySize) return;
        Collect();
    }
    public void Collect()
    {
        if (_item != null)
            Inventory.Instance.Add(_item);
        Destroy(gameObject);
    }
    public void SetItem(Item item)
    {
        _item = item;
        _spriteRenderer.sprite = item.ItemSprite;
        _spriteRenderer.color = item.ItemColor;
    }
}
