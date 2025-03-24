using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeployedGadget : MonoBehaviour
{
    public Gadget Gadget;
    public virtual void Update()
    {
        Gadget.IncreaseCharge();
    }
    public virtual void SetGadget(Gadget gadget)
    {
        Gadget = gadget;
        Gadget.GadgetState = GadgetState.Charging;
    }
    public virtual void Use()
    {
        Gadget.Charge = 0;
        Gadget.GadgetState = GadgetState.Active;
    }
    public virtual void AttemptedUseWhileWaiting()
    {
        return;
    }
    public virtual void Deactivate()
    {
        Gadget.GadgetState = GadgetState.Charging;
    }
    public virtual void AltUse()
    {
        return;
    }
    public virtual void OnGadgetSwitchedToState(GadgetState state)
    {
        return;
    }
    public virtual void Undeploy()
    {
        Destroy(gameObject);
    }
}
