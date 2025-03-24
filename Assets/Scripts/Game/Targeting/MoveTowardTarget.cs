using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardTarget : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Targeter _targeter;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Settings")]
    public float MovementSpeed;
    [Range(0, 1)] public float Acceleration;
    public float StopMovingRadius;
    [SerializeField] private bool _mirrorSpriteToDirection;

    private Vector2 _velocity;
    void FixedUpdate()
    {
        SetVelocity();
        if (_mirrorSpriteToDirection)
            FlipScaleToXTarget();
    }
    private void SetVelocity()
    {
        float speed = _targeter.DistanceToTarget <= StopMovingRadius ? 0 : MovementSpeed;
        _velocity = Vector2.Lerp(_velocity, speed * _targeter.DirectionToTargetLocation, Acceleration);
        _rigidbody.velocity = _velocity;
    }
    private void FlipScaleToXTarget()
    {
        float x = _targeter.VectorToTargetLocation.x;
        if (x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
