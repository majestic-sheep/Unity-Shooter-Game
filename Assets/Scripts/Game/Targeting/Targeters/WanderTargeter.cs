using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderTargeter : Targeter
{
    [SerializeField] private float _minimumDistance;
    [SerializeField] private float _maximumDistance;
    [SerializeField] private float _minimumDelay;
    [SerializeField] private float _maximumDelay;
    private float _lastChangeTime;
    private float _currentDelay;
    protected override void Awake()
    {
        base.Awake();
        ChooseNewTarget();
    }
    void Update()
    {
        if (Time.time >= _lastChangeTime + _currentDelay)
            ChooseNewTarget();
    }
    private void ChooseNewTarget()
    {
        float theta = Random.Range(0f, 2 * Mathf.PI);
        float r = Random.Range(_minimumDistance, _maximumDistance);
        float x = r * Mathf.Cos(theta);
        float y = r * Mathf.Sin(theta);
        TargetLocation = new Vector2(x, y);
        _lastChangeTime = Time.time;
        _currentDelay = Random.Range(_minimumDelay, _maximumDelay);
    }
}
