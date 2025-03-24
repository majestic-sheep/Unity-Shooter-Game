using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseTargeter : Targeter
{
    private Camera _camera;
    protected override void Awake()
    {
        base.Awake();
        _camera = FindAnyObjectByType<Camera>();
    }
    private void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = _camera.ScreenToWorldPoint(mouseScreenPos);
        TargetLocation = mouseWorldPos;
    }
}
