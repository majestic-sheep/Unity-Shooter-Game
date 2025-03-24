using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeployedStimDrone : DeployedGadget
{
    [Header("Stim Drone References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Health _playerHealth;
    protected virtual void Start()
    {
        _playerHealth = PlayerMovement.Instance.gameObject.GetComponent<Health>();
    }
    public override void Use()
    {
        Potion potion = (Potion)Gadget.Item;
        new Effect(_playerHealth, potion.Effect, PotionManager.Instance.GetPotionColor(potion.Effect));
        base.Use();
        Gadget.GadgetState = GadgetState.Charging;
    }
    public override void AltUse()
    {
        if (AttemptLoadPotionFromInventory())
        {
            Gadget.GadgetState = GadgetState.Charging;
            Gadget.Charge = 0f;
        }
        base.AltUse();
    }
    public override void AttemptedUseWhileWaiting()
    {
        if (AttemptLoadPotionFromInventory())
        {
            Gadget.GadgetState = GadgetState.Charging;
            Gadget.Charge = 0f;
        }
        base.AttemptedUseWhileWaiting();
    }
    private bool AttemptLoadPotionFromInventory()
    {
        Item item = Inventory.Instance.CurrentItem;
        if (item is Potion potion)
        {
            LoadPotion(potion);
            Inventory.Instance.RemoveCurrentItem();
            return true;
        }
        else
        {
            return false;
        }
    }
    private void LoadPotion(Potion potion)
    {
        Gadget.Item = potion;
        Gadget.GadgetName = $"{potion.Effect} Drone";
        Color color = PotionManager.Instance.GetPotionColor(potion.Effect);
        Gadget.GadgetNameColor = color;
        Gadget.ItemColor = color;
        _spriteRenderer.color = PotionManager.Instance.GetPotionColor(((Potion)Gadget.Item).Effect);
    }
    public override void SetGadget(Gadget gadget)
    {
        base.SetGadget(gadget);
        if (Gadget.Item != null && Gadget.Item is Potion potion)
        {
            LoadPotion(potion);
        }
        else
        {
            Gadget.GadgetState = GadgetState.Waiting;
        }
    }
}
