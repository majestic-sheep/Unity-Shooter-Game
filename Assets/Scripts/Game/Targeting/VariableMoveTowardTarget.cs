using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class VariableMoveTowardTarget : MoveTowardTarget
{
    [SerializeField] private float _minimumMovementSpeed;
    [SerializeField] private float _maximumMovementSpeed;
    [SerializeField] private float _minimumAcceleration;
    [SerializeField] private float _maximumAcceleration;
    [SerializeField] private float _minimumStopMovingRadius;
    [SerializeField] private float _maximumStopMovingRadius;
    protected virtual void Awake()
    {
        MovementSpeed = Random.Range(_minimumMovementSpeed, _maximumMovementSpeed);
        Acceleration = Random.Range(_minimumAcceleration, _maximumAcceleration);
        StopMovingRadius = Random.Range(_minimumStopMovingRadius, _maximumStopMovingRadius);
    }
}
