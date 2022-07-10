using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    /// <summary>
    /// This class handles the spawning of all turrets
    /// </summary>

    [HideInInspector] public TurretFactory factory = null;

    [SerializeField] private NormalTurretFactory normalFactory;
    [SerializeField] private HeavyTurretFactory heavyFactory;
    [SerializeField] private DebuffTurretFactory debuffFactory;

    private void Start()
    {
        if (normalFactory == null)
            throw new System.Exception("NormalTurretFactory in TurretSpawner is null!");
        if (heavyFactory == null)
            throw new System.Exception("HeavyTurretFactory in TurretSpawner is null!");
        if (debuffFactory == null)
            throw new System.Exception("DebuffTurretFactory in TurretSpawner is null!");

        // Set the switch state to do nothing
        SwitchTurret("start");
    }

    public Turret GetNormalTurret()
    {
        // Returns the regular turret
        SwitchTurret("regular");
        return factory.GetTurret();
    }

    public Turret GetHeavyTurret()
    {
        // Returns the heavy turret
        SwitchTurret("heavy");
        return factory.GetTurret();
    }

    public Turret GetDebuffTurret()
    {
        // Returns the debuff turret
        SwitchTurret("debuff");
        return factory.GetTurret();
    }

    private void SwitchTurret(string _turret)
    {
        switch (_turret.ToLower())
        {
            case "start":
                factory = null;
                break;

            case "regular":
                factory = normalFactory;
                break;

            case "heavy":
                factory = heavyFactory;
                break;

            case "debuff":
                factory = debuffFactory;
                break;
        }
    }

    //private void InfoDebug()
    //{
    //    Turret turret = factory.GetTurret();
    //    Debug.Log($"Type: {turret.TurretType}, Level: {turret.Level}, Damage: {turret.Damage}, Range: {turret.Range}");
    //}
}
