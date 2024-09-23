using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private EnemyAwareness _enemyAwareness;
    [SerializeField] private Vector2 _targetDirection;
    [SerializeField] private float _wanderDistance;
    private Vector2 _wanderTargetPoint;
    [SerializeField] private float _newWanderTargetPointDelay;
    private float _lastNewWanderTargetPointTime;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _acceleration;
    private Vector2 _velocity = Vector2.zero;
    private void Awake()
    {
        NewWanderTargetPoint();
    }
    void FixedUpdate()
    {
        UpdateTargetDirection();
        SetVelocity();
        UpdateGraphics();
    }
    private void UpdateTargetDirection()
    {
        if (_enemyAwareness.AwareOfPlayer)
        {
            _targetDirection = _enemyAwareness.DirectionToPlayer;
        }
        else
        {
            if (Time.time >= _lastNewWanderTargetPointTime + _newWanderTargetPointDelay)
                NewWanderTargetPoint();
            Vector2 enemyToPlayerVector = _wanderTargetPoint - new Vector2(transform.position.x, transform.position.y);
            if (enemyToPlayerVector.magnitude > 0.5f)
                _targetDirection = enemyToPlayerVector.normalized;
            else
                _targetDirection = Vector2.zero;
        }
    }
    private void NewWanderTargetPoint()
    {
        float newX = transform.position.x + Random.Range(-_wanderDistance, _wanderDistance);
        float newY = transform.position.y + Random.Range(-_wanderDistance, _wanderDistance);
        newX = Mathf.Clamp(newX, Constants.LEFTBORDER, Constants.RIGHTBORDER);
        newY = Mathf.Clamp(newY, Constants.BOTTOMBORDER, Constants.TOPBORDER);
        _wanderTargetPoint = new Vector2(newX, newY);
        _lastNewWanderTargetPointTime = Time.time;
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
