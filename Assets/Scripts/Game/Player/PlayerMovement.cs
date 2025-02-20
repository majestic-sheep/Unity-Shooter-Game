using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    public float MovementSpeed;
    [SerializeField] private float _acceleration;
    private Vector2 _velocity = Vector2.zero;
    private void FixedUpdate()
    {
        _velocity = Vector2.Lerp(_velocity, _movementInput * MovementSpeed, _acceleration);
        ClampMovementToScreenSize();
        _rigidbody.velocity = _velocity;
        SetAnimation();
    }
    private void OnMove(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        _movementInput = inputVector;
        FlipScaleToXInput(inputVector.x);
    }
    private void ClampMovementToScreenSize()
    {
        if (transform.position.x < Constants.LEFTBORDER)
        {
            if (_rigidbody.velocity.x < 0)
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            transform.position = new Vector2(Constants.LEFTBORDER, transform.position.y);
        }
        if (transform.position.x > Constants.RIGHTBORDER)
        {
            if (_rigidbody.velocity.x > 0)
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            transform.position = new Vector2(Constants.RIGHTBORDER, transform.position.y);
        }
        if (transform.position.y < Constants.BOTTOMBORDER)
        {
            if (_rigidbody.velocity.y < 0)
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            transform.position = new Vector2(transform.position.x, Constants.BOTTOMBORDER);
        }
        if (transform.position.y > Constants.TOPBORDER)
        {
            if (_rigidbody.velocity.y > 0)
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            transform.position = new Vector2(transform.position.x, Constants.TOPBORDER);
        }
    }
    private void SetAnimation()
    {
        _animator.SetBool("IsMoving", _movementInput.magnitude > 0);
    }
    private void FlipScaleToXInput(float xInput)
    {
        if (xInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (xInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}