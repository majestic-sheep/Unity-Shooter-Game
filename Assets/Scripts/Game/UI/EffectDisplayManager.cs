using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDisplayManager : MonoBehaviour
{
    [SerializeField] private float _effectDisplayX;
    [SerializeField] private float _effectDisplayY0;
    [SerializeField] private float _effectDisplayYIncrement;

    [SerializeField] private GameObject _effectDisplayPrefab;
    public List<Effect> Effects = new();
    private List<GameObject> _effectDisplays = new();
    public void DestroyDisplayedEffects()
    {
        foreach (GameObject obj in _effectDisplays)
        {
            Destroy(obj);
        }
    }
    public void GenerateDisplayedEffects()
    {
        float y = _effectDisplayY0;
        for (int i = 0; i < Effects.Count; i++)
        {
            GameObject effectDisplay = Instantiate(_effectDisplayPrefab, transform);
            effectDisplay.transform.localPosition = new Vector2(_effectDisplayX, y);
            EffectDisplay script = effectDisplay.GetComponent<EffectDisplay>();
            script.DisplayManager = this;
            script.SetEffect(Effects[i]);
            _effectDisplays.Add(effectDisplay);
            y += _effectDisplayYIncrement;
        }
    }
    public void AddEffectToDisplay(Effect effect)
    {
        Effects.Add(effect);
        ReloadDisplayedEffects();
    }
    public void RemoveEffect(Effect effect)
    {
        Effects.Remove(effect);
        ReloadDisplayedEffects();
    }
    public void ReloadDisplayedEffects()
    {
        DestroyDisplayedEffects();
        GenerateDisplayedEffects();
    }
}
