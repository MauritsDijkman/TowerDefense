using System.Collections;
using UnityEngine;

public class SettingValues : State
{
    public SettingValues(Shooting_StateMachine pShooting_StateMachine) : base(pShooting_StateMachine)
    {
    }

    public override IEnumerator Start()
    {
        // Get the TurretStats script and set the necessary values of the Shooting_StateMachine class to the one of the TurretStats class.

        TurretStats turretStats = _shooting_StateMachine.turret.GetComponent<TurretStats>();

        if (turretStats != null)
        {
            _shooting_StateMachine._range = turretStats._range;
            _shooting_StateMachine._fireRate = turretStats._fireRate;
            _shooting_StateMachine._damage = turretStats._damage;
        }
        else
            throw new System.Exception($"No turretStats (TurretStats.cs) script found in components of gameobject where the TargetShooting script is located.");

        yield return new WaitForSeconds(0f);
    }
}
