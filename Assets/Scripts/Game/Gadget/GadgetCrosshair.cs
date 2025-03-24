using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetCrosshair : MonoBehaviour
{
    public Targeter Targeter;
    public ShootTarget ShootTarget;
    [SerializeField] private SpriteRenderer _renderer;
    void Update()
    {
        if (Targeter.HasTarget && ShootTarget.Firing)
        {
            _renderer.enabled = true;
            gameObject.transform.position = Targeter.TargetLocation;
        }
        else
        {
            _renderer.enabled = false;
        }
    }
}
