using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GadgetType { TurretDrone, StimDrone, ForceField };
public enum GadgetState { Waiting, Charging, Ready, Active };
public class Gadget : Item
{
    public string GadgetName;
    public Color GadgetNameColor;
    public GadgetType GadgetType;
    private GadgetState _gadgetState;
    public GadgetState GadgetState
    {
        get
        {
            return _gadgetState;
        }
        set
        {
            _gadgetState = value;
            GadgetManager.Instance.OnGadgetSwitchedToState(this, value);
        }
    }
    /// <summary>
    /// A non-gadget item being held or carried
    /// </summary>
    public Item Item;
    public int Value;
    public float Charge;
    private float _chargePerSecond;
    public bool HasAltUse;
    public float SecondsRemainingToReady
    {
        get
        {
            return (1 - Charge) * _chargePerSecond;
        }
    }
    public Gadget(GadgetType type)
    {
        GadgetType = type;
        GadgetNameColor = Color.white;
        Value = -1;
        switch (type)
        {
            case GadgetType.TurretDrone:
                ItemSprite = GadgetManager.Instance.TurretDroneSprite;
                GadgetName = "Turret Drone";
                Value = 0;
                _chargePerSecond = 0.1f;
                HasAltUse = false;
                break;
            case GadgetType.StimDrone:
                ItemSprite = GadgetManager.Instance.StimDroneSprite;
                GadgetName = "Stim Drone";
                _chargePerSecond = 0.03f;
                HasAltUse = true;
                break;
            case GadgetType.ForceField:
                ItemSprite = GadgetManager.Instance.ForceFieldSprite;
                GadgetName = "Force Field";
                _chargePerSecond = 0.02f;
                HasAltUse = false;
                break;
            default:
                break;
        }
        ItemColor = Color.white;
    }
    public void Deploy()
    {
        GadgetManager.Instance.Deploy(this);
    }
    public void IncreaseCharge()
    {
        if (GadgetState != GadgetState.Charging)
            return;
        Charge += Time.deltaTime * _chargePerSecond;
        if (Charge > 1)
        {
            Charge = 1;
            GadgetState = GadgetState.Ready;
        }
    }
    public override bool Equals(Item other)
    {
        if (!base.Equals(other)) return false;

        if (other is not Gadget) return false;
        Gadget gadget = (Gadget)other;

        if (gadget.GadgetType != GadgetType) return false;
        return true;
    }
    public override Item Clone()
    {
        Gadget gadget = new(GadgetType)
        {
            ItemColor = ItemColor
        };
        return gadget;
    }
}
