using System;
using UnityEngine;

[Serializable]
public class LootTable
{
    [SerializeField] private float _dropChance;
    [SerializeField] private int _potionWeight;
    [SerializeField] private int _healthWeight;
    [SerializeField] private int _hasteWeight;
    [SerializeField] private int _powerWeight;
    [SerializeField] private int _luckWeight;
    [SerializeField] private int _stenchWeight;
    [SerializeField] private int _poisonWeight;
    [SerializeField] private int _slowWeight;
    [SerializeField] private int _lingerWeight;
    [SerializeField] private int _weaponWeight;
    [SerializeField] private int _pistolWeight;
    [SerializeField] private int _rifleWeight;
    [SerializeField] private int _shotgunWeight;
    [SerializeField] private int _smgWeight;
    [SerializeField] private float _modifierChance;
    [SerializeField] private int _fastWeight;
    [SerializeField] private int _splitWeight;
    [SerializeField] private int _heavyWeight;
    [SerializeField] private int _gadgetWeight;
    [SerializeField] private int _turretDroneWeight;
    [SerializeField] private int _stimDroneWeight;
    [SerializeField] private int _forceFieldWeight;
    public Item GetItemToDrop()
    {
        if (UnityEngine.Random.value > _dropChance) return null;

        int rand = UnityEngine.Random.Range(0, _weaponWeight + _potionWeight + _gadgetWeight);
        if (rand < _weaponWeight) return GetWeaponToDrop();
        else rand -= _weaponWeight;
        if (rand < _potionWeight) return GetPotionToDrop();
        return GetGadgetToDrop();
    }
    private Weapon GetWeaponToDrop()
    {
        return new Weapon(ChooseWeaponType(), ChooseWeaponModifier());
    }
    private WeaponType ChooseWeaponType()
    {
        int rand = UnityEngine.Random.Range(0, 
            _pistolWeight + 
            _rifleWeight + 
            _shotgunWeight +
            _smgWeight);
        if (rand < _pistolWeight) return WeaponType.Pistol;
        else rand -= _pistolWeight;
        if (rand < _rifleWeight) return WeaponType.Rifle;
        else rand -= _rifleWeight;
        if (rand < _shotgunWeight) return WeaponType.Shotgun;
        return WeaponType.SMG;
    }
    private WeaponModifierType ChooseWeaponModifier()
    {
        if (UnityEngine.Random.value > _modifierChance) return WeaponModifierType.None;

        int rand = UnityEngine.Random.Range(0, _fastWeight + _splitWeight + _heavyWeight);
        if (rand < _fastWeight) return WeaponModifierType.Fast;
        if (rand - _fastWeight < _splitWeight) return WeaponModifierType.Split;
        else return WeaponModifierType.Heavy;
    }
    private Potion GetPotionToDrop()
    {
        return new Potion(ChoosePotionEffect());
    }
    private string ChoosePotionEffect()
    {
        int rand = UnityEngine.Random.Range(0,
            _healthWeight +
            _hasteWeight +
            _powerWeight +
            _luckWeight +
            _stenchWeight +
            _poisonWeight +
            _slowWeight +
            _lingerWeight);
        if (rand < _healthWeight) return "Health";
        else rand -= _healthWeight;
        if (rand < _hasteWeight) return "Haste";
        else rand -= _hasteWeight;
        if (rand < _powerWeight) return "Power";
        else rand -= _powerWeight;
        if (rand < _luckWeight) return "Luck";
        else rand -= _luckWeight;
        if (rand < _stenchWeight) return "Stench";
        else rand -= _stenchWeight;
        if (rand < _poisonWeight) return "Poison";
        else rand -= _poisonWeight;
        if (rand < _slowWeight) return "Slow";
        else rand -= _slowWeight;
        if (rand < _lingerWeight) return "Linger";
        return "Missing Type";
    }
    private Gadget GetGadgetToDrop()
    {
        return new Gadget(ChooseGadgetType());
    }
    private GadgetType ChooseGadgetType()
    {
        int rand = UnityEngine.Random.Range(0,
            _turretDroneWeight +
            _stimDroneWeight +
            _forceFieldWeight);
        if (rand < _turretDroneWeight) return GadgetType.TurretDrone;
        else rand -= _turretDroneWeight;
        if (rand < _stimDroneWeight)
        {
            return GadgetType.StimDrone;
        }
        else return GadgetType.ForceField;
    }
    public LootTable ModifiedBy(LootTable other)
    {
        return ModifiedBy(other, 1);
    }
    public LootTable ModifiedBy(LootTable other, int magnitude)
    {
        return new LootTable()
        {
            _dropChance = _dropChance + magnitude * other._dropChance,
            _potionWeight = _potionWeight + magnitude * other._potionWeight,
            _healthWeight = _healthWeight + magnitude * other._healthWeight,
            _hasteWeight = _hasteWeight + magnitude * other._hasteWeight,
            _powerWeight = _powerWeight + magnitude * other._powerWeight,
            _luckWeight = _luckWeight + magnitude * other._luckWeight,
            _stenchWeight = _stenchWeight + magnitude * other._stenchWeight,
            _poisonWeight = _poisonWeight + magnitude * other._poisonWeight,
            _slowWeight = _slowWeight + magnitude * other._slowWeight,
            _lingerWeight = _lingerWeight + magnitude * other._lingerWeight,
            _weaponWeight = _weaponWeight + magnitude * other._weaponWeight,
            _pistolWeight = _pistolWeight + magnitude * other._pistolWeight,
            _rifleWeight = _rifleWeight + magnitude * other._rifleWeight,
            _shotgunWeight = _shotgunWeight + magnitude * other._shotgunWeight,
            _smgWeight = _smgWeight + magnitude * other._smgWeight,
            _modifierChance = _modifierChance + magnitude * other._modifierChance,
            _fastWeight = _fastWeight + magnitude * other._fastWeight,
            _splitWeight = _splitWeight + magnitude * other._splitWeight,
            _heavyWeight = _heavyWeight + magnitude * other._heavyWeight,
            _gadgetWeight = _gadgetWeight + magnitude * other._gadgetWeight,
            _turretDroneWeight = _turretDroneWeight + magnitude * other._turretDroneWeight,
            _stimDroneWeight = _stimDroneWeight + magnitude * other._stimDroneWeight,
            _forceFieldWeight = _forceFieldWeight + magnitude * other._forceFieldWeight,
        };
    }
}
