using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropCollectable : MonoBehaviour
{
    [SerializeField] private GameObject _collectablePrefab;

    [SerializeField] private float _dropChance;
    [SerializeField] private float _pistolWeight;
    [SerializeField] private float _rifleWeight;
    [SerializeField] private float _shotgunWeight;
    [SerializeField] private float _modifierChance;
    [SerializeField] private float _fastWeight;
    [SerializeField] private float _splitWeight;
    [SerializeField] private float _heavyWeight;

    private float _dropYOffset;
    private void Start()
    {
        _dropYOffset = GetComponent<Transform>().localScale.y * GetComponent<SpriteRenderer>().size.y * -0.4f;
    }
    public void DropCollectable()
    {
        if (Random.value > _dropChance) return;

        Weapon weapon = new(ChooseType(), ChooseModifier());
        GameObject _collectable = Instantiate(_collectablePrefab, transform.position, Quaternion.identity);
        _collectable.transform.position = new Vector2(
            _collectable.transform.position.x,
            _collectable.transform.position.y + _dropYOffset);
        _collectable.GetComponent<CollectableWeapon>().SetWeapon(weapon);
    }
    private string ChooseType()
    {
        float rand = Random.Range(0, _pistolWeight + _rifleWeight + _shotgunWeight);
        if (rand <= _pistolWeight) return "pistol";
        if (rand <= _pistolWeight + _rifleWeight) return "rifle";
        else return "shotgun";
    }
    private string ChooseModifier()
    {
        if (Random.value > _modifierChance) return "";

        float rand = Random.Range(0, _fastWeight + _splitWeight + _heavyWeight);
        if (rand <= _fastWeight) return "fast";
        if (rand <= _fastWeight + _splitWeight) return "split";
        else return "heavy";
    }
}
