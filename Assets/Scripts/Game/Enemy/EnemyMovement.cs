using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private EnemyAwareness _enemyAwareness;
    [SerializeField] private Vector2 _targetDirection;

    private Vector2 _movementInput;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _acceleration;
    private Vector2 _velocity = Vector2.zero;
    void FixedUpdate()
    {
        UpdateTargetDirection();
        SetVelocity();
        UpdateGraphics();
    }
    private void UpdateTargetDirection()
    {
        if (_enemyAwareness.AwareOfPlayer)
            _targetDirection = _enemyAwareness.DirectionToPlayer;
        else
            _targetDirection = Vector2.zero;
    }
    private void SetVelocity()
    {
        _velocity = Vector2.Lerp(_velocity, _movementSpeed * _targetDirection, _acceleration);
        _rigidbody.velocity = _velocity;
    }
    private void UpdateGraphics()
    {
        if (_targetDirection == Vector2.zero) return;
        if (_targetDirection.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
