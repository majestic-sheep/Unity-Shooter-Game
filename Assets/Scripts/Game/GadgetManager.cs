using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;
using System.Reflection;

public class GadgetManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _canvasTransform;

    [SerializeField] private GameObject _gadgetDisplay;

    [Header("Turret Drone")]
    [SerializeField] private GameObject _turretDrone;
    public Sprite TurretDroneSprite;

    [Header("Stim Drone")]
    [SerializeField] private GameObject _stimDrone;
    public Sprite StimDroneSprite;

    [Header("Force Field")]
    [SerializeField] private GameObject _forceField;
    public Sprite ForceFieldSprite;

    private List<DeployedGadget> _deployedGadgets = new();
    private List<GadgetDisplay> _gadgetDisplays = new();
    public static GadgetManager Instance { get; private set; }
    public float InitialDisplayXOffset;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void UseCurrentGadget()
    {
        if (_deployedGadgets.Count == 0)
            return;
        if (_deployedGadgets[0].Gadget.GadgetState == GadgetState.Ready)
            _deployedGadgets[0].Use();
        else if (_deployedGadgets[0].Gadget.GadgetState == GadgetState.Waiting)
            _deployedGadgets[0].AttemptedUseWhileWaiting();
        else
            return;
    }
    public void AltUseGadget(Gadget gadget)
    {
        int index = IndexOf(gadget);
        _deployedGadgets[index].AltUse();
    }
    public void Deploy(Gadget gadget)
    {
        GameObject prefab = null;
        switch (gadget.GadgetType)
        {
            case GadgetType.TurretDrone:
                prefab = _turretDrone;
                break;
            case GadgetType.StimDrone:
                prefab = _stimDrone;
                break;
            case GadgetType.ForceField:
                prefab = _forceField;
                break;
        }
        if (prefab != null)
        {
            GameObject deployedGadget = Instantiate(prefab, PlayerMovement.Instance.gameObject.transform.position, Quaternion.identity);
            DeployedGadget deployedGadgetScript = deployedGadget.GetComponent<DeployedGadget>();
            deployedGadgetScript.SetGadget(gadget);
            _deployedGadgets.Add(deployedGadgetScript);

            GameObject displayObject = Instantiate(_gadgetDisplay, _canvasTransform);
            displayObject.transform.localPosition = GetInitialPositionOfGadgetDisplayAtIndex(_gadgetDisplays.Count);
            GadgetDisplay gadgetDisplayScript = displayObject.GetComponent<GadgetDisplay>();
            gadgetDisplayScript.DisplayedPosition = GetInitialPositionOfGadgetDisplayAtIndex(_gadgetDisplays.Count);
            gadgetDisplayScript.TargetPosition = GetTargetPositionOfGadgetDisplayAtIndex(_gadgetDisplays.Count);
            gadgetDisplayScript.SetGadget(gadget);
            _gadgetDisplays.Add(gadgetDisplayScript);
        }
    }
    public void OnGadgetSwitchedToState(Gadget gadget, GadgetState state)
    {
        int index = IndexOf(gadget);
        if (index == -1)
            return;
        _deployedGadgets[index].OnGadgetSwitchedToState(state);
        ReorderGadgetDisplayWithGadget(gadget);
    }
    private Vector2 GetTargetPositionOfGadgetDisplayAtIndex(int index)
    {
        float displayObjectX = 800f;
        float displayObjectY = 394.3f - index * 50f;
        Vector2 displayPosition = new(displayObjectX, displayObjectY);
        return displayPosition;
    }
    private void SetAllTargetPositions()
    {
        for (int i = 0; i < _gadgetDisplays.Count; i++)
        {
            _gadgetDisplays[i].TargetPosition = GetTargetPositionOfGadgetDisplayAtIndex(i);
        }
    }
    private Vector2 GetInitialPositionOfGadgetDisplayAtIndex(int index)
    {
        float x = 800f + InitialDisplayXOffset;
        float y = 394.3f - index * 50f;
        Vector2 position = new(x, y);
        return position;
    }
    public void ReorderGadgetDisplayAtIndex(int index)
    {
        float secondsRemaining = _gadgetDisplays[index].Gadget.SecondsRemainingToReady;
        GadgetDisplay gadgetDisplay = _gadgetDisplays[index];
        _gadgetDisplays.RemoveAt(index);
        DeployedGadget deployedGadget = _deployedGadgets[index];
        _deployedGadgets.RemoveAt(index);

        int i = 0;
        for (; i < _gadgetDisplays.Count; i++)
        {
            if (_gadgetDisplays[i].Gadget.SecondsRemainingToReady > secondsRemaining)
                break;
        }
        _gadgetDisplays.Insert(i, gadgetDisplay);
        _deployedGadgets.Insert(i, deployedGadget);
        SetAllTargetPositions();
    }
    public void ReorderGadgetDisplayWithGadget(Gadget gadget)
    {
        int index = IndexOf(gadget);
        if (index >= 0)
            ReorderGadgetDisplayAtIndex(index);
    }
    private void RemoveGadget(Gadget gadget, int index)
    {
        Inventory.Instance.Add(gadget);
        _gadgetDisplays[index].Undeploy();
        _gadgetDisplays.RemoveAt(index);
        SetAllTargetPositions();
        _deployedGadgets[index].Undeploy();
        _deployedGadgets.RemoveAt(index);
    }
    public void RemoveGadget(Gadget gadget, GadgetDisplay gadgetDisplay)
    {
        RemoveGadget(gadget, IndexOf(gadgetDisplay));
    }
    private void SetGadgetToPrimary(int index)
    {
        GadgetDisplay gadgetDisplay = _gadgetDisplays[index];
        _gadgetDisplays.RemoveAt(index);
        DeployedGadget deployedGadget = _deployedGadgets[index];
        _deployedGadgets.RemoveAt(index);

        _gadgetDisplays.Insert(0, gadgetDisplay);
        _deployedGadgets.Insert(0, deployedGadget);
        SetAllTargetPositions();
    }
    public void SetGadgetToPrimary(GadgetDisplay gadgetDisplay)
    {
        SetGadgetToPrimary(IndexOf(gadgetDisplay));
    }
    public void SetGadgetToPrimary(DeployedGadget deployedGadget)
    {
        SetGadgetToPrimary(IndexOf(deployedGadget));
    }
    public void SetGadgetToPrimary(Gadget gadget)
    {
        SetGadgetToPrimary(IndexOf(gadget));
    }
    public int IndexOf(GadgetDisplay gadgetDisplay)
    {
        for (int i = 0; i < _gadgetDisplays.Count; i++)
            if (_gadgetDisplays[i] == gadgetDisplay)
                return i;
        return -1;
    }
    public int IndexOf(DeployedGadget deployedGadget)
    {
        for (int i = 0; i < _deployedGadgets.Count; i++)
            if (_deployedGadgets[i] == deployedGadget)
                return i;
        return -1;
    }
    public int IndexOf(Gadget gadget)
    {
        for (int i = 0; i < _deployedGadgets.Count; i++)
            if (_deployedGadgets[i].Gadget == gadget)
                return i;
        return -1;
    }
}