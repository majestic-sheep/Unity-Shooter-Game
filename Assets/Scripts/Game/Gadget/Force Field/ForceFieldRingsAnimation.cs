using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldRingsAnimation : MonoBehaviour
{
    [SerializeField] private List<GameObject> _rings = new();
    [SerializeField] private List<float> _offsets = new();
    [SerializeField] private float _normalSize;
    [SerializeField] private float _sizeVariance;
    void Update()
    {
        for (int i = 0; i < _rings.Count; i++)
        {
            _rings[i].transform.localScale = (_normalSize + _sizeVariance * Mathf.Sin(Time.time + _offsets[i])) * Vector3.one;
        }
    }
}
