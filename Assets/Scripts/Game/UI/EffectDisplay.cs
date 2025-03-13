using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EffectDisplay : MonoBehaviour
{
    public Effect Effect;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _durationText;
    public void Update()
    {
        if (Effect != null) _durationText.text = $":{Effect.RemainingSeconds}";
    }
    public void SetEffect(Effect effect)
    {
        Effect = effect;
        _nameText.text = effect.EffectName;
        _nameText.color = effect.Color;
        _durationText.color = effect.Color;
    }
}
