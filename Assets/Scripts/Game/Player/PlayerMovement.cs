using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem; // Allows for input to be read

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public Transform Transform;
    private Vector2 _movementInput;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _acceleration;
    private Vector2 _velocity = Vector2.zero;
    private void FixedUpdate()
    {
        _velocity = Vector2.Lerp(_velocity, _movementInput * _movementSpeed, _acceleration);
        Rigidbody.velocity = _velocity;
    }
    private void OnMove(InputValue inputValue) // Relies on UnityEngine.InputSystem
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        _movementInput = inputVector;
        if (inputVector.x > 0)
        {
            Transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputVector.x < 0)
        {
            Transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}