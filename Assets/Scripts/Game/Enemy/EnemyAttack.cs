using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackDelay;
    private float _lastAttackTime;
    private void Awake()
    {
        _lastAttackTime = Time.time - _attackDelay;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<PlayerMovement>()) return;
        if (Time.time < _lastAttackTime + _attackDelay) return;

        Health playerHealth = collision.gameObject.GetComponent<Health>();
        playerHealth.Damage(_attackDamage);
        _lastAttackTime = Time.time;
    }
}
