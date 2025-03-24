using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargeter : Targeter
{
    private Transform _playerTransform;
    private void Start()
    {
        _playerTransform = PlayerMovement.Instance.gameObject.transform;
    }
    private void Update()
    {
        TargetLocation = _playerTransform.position;
    }
}
