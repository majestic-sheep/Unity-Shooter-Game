using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    private PotionManager _potion_manager;
    public string Effect;
    public Potion(string effect)
    {
        _potion_manager = FindAnyObjectByType<PotionManager>();
        ItemSprite = _potion_manager.PotionSprite;
        ItemColor = _potion_manager.GetPotionColor(effect);
        Effect = effect;
    }
}
