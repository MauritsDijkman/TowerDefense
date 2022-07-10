using UnityEngine;

class DebuffTurret : Turret
{
    /// <summary>
    /// This class inherits from the Turret class and sets all the values from this class to the Turret class (override function)
    /// </summary>

    private GameObject _turretModel;
    private GameObject _upgradedTurretModel;
    private readonly string _turretType;
    private int _level;
    private float _damage;
    private float _range;
    private float _fireRate;
    private int _buyCost;
    private int _upgradeCost;
    private int _sellCost;
    
    public DebuffTurret(GameObject turretModel, GameObject upgradedTurretModel, int level, float damage, float range, float fireRate, int buyCost, int upgradeCost, int sellCost)
    {
        _turretModel = turretModel;
        _upgradedTurretModel = upgradedTurretModel;
        _turretType = "DebuffTurret";
        _level = level;
        _damage = damage;
        _range = range;
        _fireRate = fireRate;
        _buyCost = buyCost;
        _upgradeCost = upgradeCost;
        _sellCost = sellCost;
    }

    public override GameObject TurretModel
    {
        get { return _turretModel; }
        set { _turretModel = value; }
    }

    public override GameObject UpgradedTurretModel
    {
        get { return _upgradedTurretModel; }
        set { _upgradedTurretModel = value; }
    }

    public override string TurretType
    {
        get { return _turretType; }
    }

    public override int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public override float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public override float Range
    {
        get { return _range; }
        set { _range = value; }
    }

    public override float FireRate
    {
        get { return _fireRate; }
        set { _fireRate = value; }
    }

    public override int BuyCost
    {
        get { return _buyCost; }
        set { _buyCost = value; }
    }

    public override int UpgradeCost
    {
        get { return _upgradeCost; }
        set { _upgradeCost = value; }
    }

    public override int SellCost
    {
        get { return _sellCost; }
        set { _sellCost = value; }
    }
}
