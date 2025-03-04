using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public Sprite PotionSprite;
    private string[] _effect_names;
    private Color[] _colors;

    [SerializeField] private EffectDisplayManager _effectDisplayManager;
    [SerializeField] private Health _playerHealth;
    public void Start()
    {
        _effect_names = new string[] { "Health", "Haste", "Power", "Luck", "Stench", "Poison", "Slow", "Linger" };
        ShuffleColors();
    }
    public void ShuffleColors()
    {
        _colors = new Color[]
        {
            Color.red,
            new(0.988f, 0.466f, 0.012f),
            Color.yellow,
            Color.green,
            Color.cyan,
            Color.blue,
            new(0.588f, 0.012f, 1f),
            new(1f, 0.012f, 0.784f),
        };
        for (int i = 0; i < _colors.Length; i++)
        {
            Color tmp = _colors[i];
            int r = Random.Range(i, _colors.Length);
            _colors[i] = _colors[r];
            _colors[r] = tmp;
        }
    }
    public Color GetPotionColor(string effect)
    {
        for (int i = 0; i < _effect_names.Length; i++)
        {
            if (_effect_names[i].Equals(effect))
            {
                return _colors[i];
            }
        }
        return Color.black;
    }
    public void ExecutePotionEffect(Potion potion)
    {
        Color color = potion.ItemColor;
        string effectName = potion.Effect;
        Effect effect = new Effect(_effectDisplayManager, _playerHealth, effectName, color);
    }
}
