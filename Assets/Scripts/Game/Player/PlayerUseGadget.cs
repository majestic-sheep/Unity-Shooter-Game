using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUseGadget : MonoBehaviour
{
    private void OnUseGadget()
    {
        GadgetManager.Instance.UseCurrentGadget();
    }
}
