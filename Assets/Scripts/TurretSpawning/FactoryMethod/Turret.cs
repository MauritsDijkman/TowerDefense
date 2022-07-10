using UnityEngine;

public abstract class Turret
{
    /// <summary>
    /// This abstract class contains all basic values of all turret types
    /// </summary>

    public abstract GameObject TurretModel { get; set; }
    public abstract GameObject UpgradedTurretModel { get; set; }
    public abstract string TurretType { get; }
    public abstract int Level { get; set; }
    public abstract float Damage { get; set; }
    public abstract float Range { get; set; }
    public abstract float FireRate { get; set; }
    public abstract int BuyCost { get; set; }
    public abstract int UpgradeCost { get; set; }
    public abstract int SellCost { get; set; }
}
