using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class HeldGunPositioning : MonoBehaviour
{
    public Camera Camera;
    public Transform PlayerTransform;
    public Transform Transform;
    public Transform GraphicsTransform;
    private float defaultScale;
    private float defaultXOffset;
    // Start is called before the first frame update
    void Awake()
    {
        defaultScale = Transform.localScale.y;
        defaultXOffset = Transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.ScreenToWorldPoint(mouseScreenPos);
        if (mouseWorldPos.x < PlayerTransform.position.x ^ PlayerTransform.localScale.x < 0)
        {
            Transform.localScale = new Vector3(-defaultScale, defaultScale, 1);
            Transform.localPosition = new Vector3(-defaultXOffset,
                Transform.localPosition.y,
                Transform.localPosition.z);
        }
        else
        {
            Transform.localScale = new Vector3(defaultScale, defaultScale, 1);
            Transform.localPosition = new Vector3(defaultXOffset,
                Transform.localPosition.y,
                Transform.localPosition.z);
        }
        float xDif = mouseWorldPos.x - Transform.position.x;
        float yDif = mouseWorldPos.y - Transform.position.y;
        float angle = Mathf.Atan(yDif / xDif) * Mathf.Rad2Deg;
        if (xDif * Mathf.Sign(Transform.lossyScale.x) < 0) angle += 180; // This line was blind trial-and-error
        Transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
