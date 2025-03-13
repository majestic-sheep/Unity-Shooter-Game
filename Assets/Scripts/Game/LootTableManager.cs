using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTableManager : MonoBehaviour
{
    public static LootTableManager Instance { get; private set; }

    public float DropChanceModifier;
    public float ModifierChanceModifier;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
