using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyExclaim : MonoBehaviour
{
    [SerializeField] private EnemyAwareness _enemyAwareness;
    [SerializeField] private Health _enemyHealth;
    [SerializeField] private float _alphaDecrementRate;
    private bool exclaimed;
    private TextMeshProUGUI _textMeshPro;
    void Start()
    {
        exclaimed = false;
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (_enemyAwareness.AwareOfPlayer && !exclaimed && _enemyHealth.PercentHealth != 0)
        {
            exclaimed = true;
            _textMeshPro.alpha = 1;
        }
        if (!_enemyAwareness.AwareOfPlayer) exclaimed = false;
        _textMeshPro.alpha -= _alphaDecrementRate * Time.deltaTime;
    }
}
