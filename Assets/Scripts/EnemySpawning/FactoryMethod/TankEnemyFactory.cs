using UnityEngine;

public class TankEnemyFactory : EnemyFactory
{
    /// <summary>
    /// This class inherits from the EnemyFactory class
    /// This script is set to an empty gameobject in the scene. There, the values can be set by the developer
    /// </summary>

    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject _enemyModel;
    [SerializeField] private int _level;
    [SerializeField] private int _health;
    [SerializeField] private int _speed;
    [SerializeField] private int _worth;
    [SerializeField] private int _damage;

    public override Enemy GetEnemy()
    {
        // Return a tank enemy with the given values
        return new TankEnemy(_enemyModel, _level, _health, _speed, _worth, _damage);
    }

    public int ReturnStartValue(string valueText)
    {
        if (valueText.ToLower() == "level")
            return _level;
        else if (valueText.ToLower() == "health")
            return _health;
        else if (valueText.ToLower() == "speed")
            return _speed;
        else if (valueText.ToLower() == "worth")
            return _worth;
        else if (valueText.ToLower() == "damage")
            return _damage;

        else
        {
            Debug.Log("Given value text is not registred in the function!");
            return 0;
        }
    }
}
