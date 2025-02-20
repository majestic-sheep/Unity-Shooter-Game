using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : ScriptableObject
{
    public Sprite ItemSprite;
    public Color ItemColor;
    public virtual bool Equals(Item other)
    {
        if (other == null) return false;
        if (ItemSprite != other.ItemSprite) return false;
        if (ItemColor != other.ItemColor) return false;
        return true;
    }
    public virtual Item Clone()
    {
        Item item = new Item();
        item.ItemSprite = ItemSprite;
        item.ItemColor = ItemColor;
        return item;
    }
}
