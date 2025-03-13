using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAwareness : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer {  get; private set; }
    [SerializeField] private float _playerAwarenessDistance;
    [SerializeField] private float _stopChasingDistance;
    void Update()
    {
        Vector2 enemyToPlayerVector = PlayerMovement.Instance.transform.position - transform.position;
        if (enemyToPlayerVector.magnitude <= _stopChasingDistance)
            DirectionToPlayer = Vector2.zero;
        else
            DirectionToPlayer = enemyToPlayerVector.normalized;
        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
            NoticePlayer();
    }
    public void NoticePlayer()
    {
        AwareOfPlayer = true;
    }
}
