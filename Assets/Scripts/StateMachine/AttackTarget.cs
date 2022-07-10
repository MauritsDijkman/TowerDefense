using System.Collections;
using UnityEngine;

/// <summary>
/// This class inherits from the Stats.cs script. It overrides the start method with it's own start method.
/// The constructor is added, so that the values of the Shooting_StateMachine can be accessed.
/// </summary>

public class AttackTarget : State
{
    public AttackTarget(Shooting_StateMachine pShooting_StateMachine) : base(pShooting_StateMachine)
    {
    }

    public override IEnumerator Attack()
    {
        if (_shooting_StateMachine.bulletPrefab != null && _shooting_StateMachine.firePoint != null)
        {
            // Spawn the bullet on the fire point
            GameObject bulletGameObject = _shooting_StateMachine.SpawnObject(_shooting_StateMachine.bulletPrefab, _shooting_StateMachine.firePoint, _shooting_StateMachine.firePoint);

            // Set the damage and target of the bullet
            Bullet bullet = bulletGameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetDamage(_shooting_StateMachine._damage);
                bullet.SetTarget(_shooting_StateMachine.target);
            }
            else
                throw new System.Exception($"No bullet (Bullet.cs) script found in components of gameobject where the TargetShooting script is located.");
        }

        // If shoot particle is not null, spawn the particle on the given firepoint
        if (_shooting_StateMachine.shootParticle != null)
            _shooting_StateMachine.SpawnObject(_shooting_StateMachine.shootParticle, _shooting_StateMachine.firePoint, _shooting_StateMachine.firePoint);

        // Need to return something, in this case nothing that influcences the code
        yield return new WaitForSeconds(0f);
    }
}
