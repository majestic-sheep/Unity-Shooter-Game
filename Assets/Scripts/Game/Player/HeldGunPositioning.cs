using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class HeldGunPositioning : MonoBehaviour
{
    public static HeldGunPositioning Instance { get; private set; }

    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _graphicsTransform;
    private float _defaultScale;
    private float _defaultXOffset;
    public float angle;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _defaultScale = transform.localScale.y;
        _defaultXOffset = transform.position.x;
    }
    void Update()
    {
        if (PauseManager.Instance.Paused)
        {
            return;
        }
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = _camera.ScreenToWorldPoint(mouseScreenPos);
        if (mouseWorldPos.x < _playerTransform.position.x ^ _playerTransform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-_defaultScale, _defaultScale, 1);
            transform.localPosition = new Vector3(-_defaultXOffset,
                transform.localPosition.y,
                transform.localPosition.z);
        }
        else
        {
            transform.localScale = new Vector3(_defaultScale, _defaultScale, 1);
            transform.localPosition = new Vector3(_defaultXOffset,
                transform.localPosition.y,
                transform.localPosition.z);
        }
        float xDif = mouseWorldPos.x - transform.position.x;
        float yDif = mouseWorldPos.y - transform.position.y;
        angle = Mathf.Atan(yDif / xDif) * Mathf.Rad2Deg;
        if (xDif * Mathf.Sign(transform.lossyScale.x) < 0) angle += 180; // This line was blind trial-and-error
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
