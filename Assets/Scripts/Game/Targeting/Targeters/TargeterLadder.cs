using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargeterLadder : Targeter
{
    public List<Targeter> Targeters = new();
    private void Update()
    {
        foreach (Targeter targeter in Targeters)
            if (targeter.HasTarget)
                TargetLocation = targeter.TargetLocation;
    }
}
