using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectTargeter : Targeter
{
    public GameObject Target;
    protected void Update()
    {
        HasTarget = Target != null;
        if (Target != null)
            TargetLocation = Target.transform.position;
        else
            TargetLocation = Vector3.zero;
    }
}
