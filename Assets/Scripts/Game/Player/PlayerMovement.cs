using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Allows for input to be read

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    private Vector2 _movementInput;
    [SerializeField] private float _movementSpeed = 4.5f;
    [SerializeField] private float _acceleration = 0.5f;
    private Vector2 _velocity = Vector2.zero;
    private void FixedUpdate()
    {
        _velocity = Vector2.Lerp(_velocity, _movementInput * _movementSpeed, _acceleration);
        Rigidbody.velocity = _velocity;
    }
    private void OnMove(InputValue inputValue) // Relies on UnityEngine.InputSystem
    {
        _movementInput = inputValue.Get<Vector2>();
    }
}
