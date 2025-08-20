using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rigidbody;
    public Vector2 Velocity = Vector2.zero;
    [SerializeField] protected float _acceleration;
}
