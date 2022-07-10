using UnityEngine;

public class TurretStats : MonoBehaviour
{
    public string _type;
    public int _level;
    public float _damage;
    public float _range;
    public float _fireRate;
    public int _buyCost;
    public int _upgradeCost;
    public int _sellCost;
    public GameObject _upgradedTurretModel;

    public void SetValues(string type, int level, float damage, float range, float fireRate, int buyCost, int upgradeCost, int sellCost, GameObject upgradedTurretModel)
    {
        // This function sets all the stats to the given stats
        _type = type;
        _level = level;
        _damage = damage;
        _range = range;
        _fireRate = fireRate;
        _buyCost = buyCost;
        _upgradeCost = upgradeCost;
        _sellCost = sellCost;
        _upgradedTurretModel = upgradedTurretModel;
    }
}
