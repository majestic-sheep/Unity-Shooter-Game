using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPauseRelay : MonoBehaviour
{
    void OnEsc()
    {
        PauseManager.Instance.OnEsc();
    }
}
