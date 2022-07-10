using UnityEngine;

public class NormalTurretFactory : TurretFactory
{
    /// <summary>
    /// This class inherits from the TurretFactory class
    /// This script is set to an empty gameobject in the scene. There, the values can be set by the developer
    /// </summary>

    [SerializeField] private GameObject _turretModel;
    [SerializeField] private GameObject _upgradedTurretModel;
    [SerializeField] private int _level;
    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _fireRate;
    [SerializeField] private int _buyCost;
    [SerializeField] private int _upgradeCost;
    [SerializeField] private int _sellCost;

    public override Turret GetTurret()
    {
        // Spawn the enemy on the given spawnposition and place it in the 'Turret' list
        GameObject turretModel = Instantiate(_turretModel, transform.position, transform.rotation);

        if (GameObject.Find("Turrets") != null)
            turretModel.transform.parent = GameObject.Find("Turrets").transform;

        // Add the FollowMouse script to the gameobject
        turretModel.AddComponent<FollowMouse>();

        // Return a normal turret with the given values
        return new NormalTurret(_turretModel, _upgradedTurretModel, _level, _damage, _range, _fireRate, _buyCost, _upgradeCost, _sellCost);
    }

    public int ReturnBuyCost()
    {
        return _buyCost;
    }
}
