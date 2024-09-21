using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class HeldGunPositioning : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _graphicsTransform;
    private float _defaultScale;
    private float _defaultXOffset;
    // Start is called before the first frame update
    void Awake()
    {
        _defaultScale = transform.localScale.y;
        _defaultXOffset = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
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
        float angle = Mathf.Atan(yDif / xDif) * Mathf.Rad2Deg;
        if (xDif * Mathf.Sign(transform.lossyScale.x) < 0) angle += 180; // This line was blind trial-and-error
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
