using UnityEngine;

class TankEnemy : Enemy
{
    /// <summary>
    /// This class inherits from the Enemy class and sets all the values from this class to the Enemy class (override function)
    /// </summary>

    private GameObject _enemyModel;
    private readonly string _enemyType;
    private int _level;
    private float _health;
    private float _speed;
    private float _worth;
    private float _damage;

    public TankEnemy(GameObject enemyModel, int level, float health, float speed, float worth, float damage)
    {
        _enemyModel = enemyModel;
        _enemyType = "TankEnemy";
        _level = level;
        _health = health;
        _speed = speed;
        _worth = worth;
        _damage = damage;
    }

    public override GameObject EnemyModel
    {
        get { return _enemyModel; }
        set { _enemyModel = value; }
    }

    public override string EnemyType
    {
        get { return _enemyType; }
    }

    public override int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public override float Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public override float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public override float Worth
    {
        get { return _worth; }
        set { _worth = value; }
    }

    public override float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
}
