using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PreventScreenClicks : EventTrigger
{
    private bool _mouseIsOverElement;
    public override void OnPointerEnter(PointerEventData eventData)
    {
        ScreenClickManager.Instance.MouseIsOverScreen = false;
        _mouseIsOverElement = true;
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        ScreenClickManager.Instance.MouseIsOverScreen = true;
        _mouseIsOverElement = false;
    }
    private void OnDisable()
    {
        if (_mouseIsOverElement)
            ScreenClickManager.Instance.MouseIsOverScreen = true;
    }
    private void OnDestroy()
    {
        if (_mouseIsOverElement)
            ScreenClickManager.Instance.MouseIsOverScreen = true;
    }
}
