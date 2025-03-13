using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    public string Effect;
    public Potion(string effect)
    {
        ItemSprite = PotionManager.Instance.PotionSprite;
        ItemColor = PotionManager.Instance.GetPotionColor(effect);
        Effect = effect;
    }
}
