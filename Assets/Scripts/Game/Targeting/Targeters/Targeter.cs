using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public Vector2 TargetLocation;
    public Vector2 VectorToTargetLocation
    {
        get
        {
            return TargetLocation - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        }
    }
    public Vector2 DirectionToTargetLocation
    {
        get
        {
            return VectorToTargetLocation.normalized;
        }
    }
    public float DistanceToTarget
    {
        get
        {
            return VectorToTargetLocation.magnitude;
        }
    }
    public bool HasTarget;
    protected virtual void Awake()
    {
        HasTarget = true;
    }
}
