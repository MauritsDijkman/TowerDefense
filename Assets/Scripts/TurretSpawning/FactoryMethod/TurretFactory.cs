using UnityEngine;

public abstract class TurretFactory : MonoBehaviour
{
    /// <summary>
    /// This class is the general turret factory and has the abstract Turret class
    /// </summary>
    /// 

    public abstract Turret GetTurret();
}
