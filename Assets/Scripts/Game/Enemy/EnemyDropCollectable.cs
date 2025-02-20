using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropCollectable : MonoBehaviour
{
    [SerializeField] private GameObject _collectablePrefab;
    [SerializeField] private LootTableManager _lootTableManager;
    [SerializeField] private float _dropChance;
    [SerializeField] private float _potionWeight;
    [SerializeField] private float _healthWeight;
    [SerializeField] private float _hasteWeight;
    [SerializeField] private float _powerWeight;
    [SerializeField] private float _luckWeight;
    [SerializeField] private float _stenchWeight;
    [SerializeField] private float _poisonWeight;
    [SerializeField] private float _slowWeight;
    [SerializeField] private float _weaponWeight;
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
        _lootTableManager = FindAnyObjectByType<LootTableManager>();
    }
    public void DropCollectable()
    {
        if (Random.value > _dropChance + _lootTableManager.DropChanceModifier) return;

        float rand = Random.Range(0, _potionWeight + _weaponWeight);
        if (rand <= _potionWeight) DropPotion();
        else DropWeapon();
    }
    public void DropPotion()
    {
        string effect = ChoosePotionEffect();
        Potion potion = new(effect);
        CreateCollectable(potion);
    }
    private string ChoosePotionEffect()
    {
        float rand = Random.Range(0,
            _healthWeight +
            _hasteWeight +
            _powerWeight +
            _luckWeight +
            _stenchWeight +
            _poisonWeight +
            _slowWeight);
        if (rand <= _healthWeight) return "Health";
        else rand -= _healthWeight;
        if (rand <= _hasteWeight) return "Haste";
        else rand -= _hasteWeight;
        if (rand <= _powerWeight) return "Power";
        else rand -= _powerWeight;
        if (rand <= _luckWeight) return "Luck";
        else rand -= _luckWeight;
        if (rand <= _stenchWeight) return "Stench";
        else rand -= _stenchWeight;
        if (rand <= _poisonWeight) return "Poison";
        else rand -= _poisonWeight;
        if (rand <= _slowWeight) return "Slow";
        return "Missing Type";
    }
    public void DropWeapon()
    {
        Weapon weapon = new(ChooseWeaponType(), ChooseWeaponModifier());
        CreateCollectable(weapon);
    }
    private string ChooseWeaponType()
    {
        float rand = Random.Range(0, _pistolWeight + _rifleWeight + _shotgunWeight);
        if (rand <= _pistolWeight) return "pistol";
        if (rand <= _pistolWeight + _rifleWeight) return "rifle";
        else return "shotgun";
    }
    private string ChooseWeaponModifier()
    {
        if (Random.value > _modifierChance + _lootTableManager.ModifierChanceModifier) return "";

        float rand = Random.Range(0, _fastWeight + _splitWeight + _heavyWeight);
        if (rand <= _fastWeight) return "fast";
        if (rand <= _fastWeight + _splitWeight) return "split";
        else return "heavy";
    }
    public void CreateCollectable(Item item)
    {
        GameObject _collectable = Instantiate(_collectablePrefab, transform.position, Quaternion.identity);
        _collectable.transform.position = new Vector2(
            _collectable.transform.position.x,
            _collectable.transform.position.y + _dropYOffset);
        _collectable.GetComponent<CollectableBehavior>().SetItem(item);
    }
}
