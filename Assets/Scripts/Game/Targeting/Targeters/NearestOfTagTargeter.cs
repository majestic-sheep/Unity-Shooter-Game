using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestOfTagTargeter : GameObjectTargeter
{
    public string Tag;
    protected new void Update()
    {
        float lowestDistance = float.MaxValue;
        GameObject closestTarget = null;

        foreach (GameObject potentialTarget in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float distance = Vector2.Distance(gameObject.transform.position, potentialTarget.transform.position);
            if (distance < lowestDistance)
            {
                lowestDistance = distance;
                closestTarget = potentialTarget;
            }
        }

        if (closestTarget != null)
            Target = closestTarget;
        else
            Target = null;

        base.Update();
    }
}
