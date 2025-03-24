using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenClickManager : MonoBehaviour
{
    public static ScreenClickManager Instance { get; private set; }
    public bool MouseIsOverScreen;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        MouseIsOverScreen = true;
    }
}
