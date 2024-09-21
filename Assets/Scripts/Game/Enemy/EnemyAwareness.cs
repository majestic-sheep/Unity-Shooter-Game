using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAwareness : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer {  get; private set; }
    [SerializeField] private float _playerAwarenessDistance;
    private Transform _playerTransform;
    private void Awake()
    {
        // As a prefab, it cannot keep editor-assigned serialized variables referenced to external components
        _playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }
    void Update()
    {
        Vector2 enemyToPlayerVector = _playerTransform.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;
        AwareOfPlayer = enemyToPlayerVector.magnitude <= _playerAwarenessDistance;
    }
}
