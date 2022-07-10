using System.Collections;
using UnityEngine;

/// <summary>
/// This class inherits from the Stats.cs script. It overrides the start method with it's own start method.
/// The constructor is added, so that the values of the Shooting_StateMachine can be accessed.
/// </summary>

public class SearchTarget : State
{
    public SearchTarget(Shooting_StateMachine pShooting_StateMachine) : base(pShooting_StateMachine)
    {
    }

    public override IEnumerator Search()
    {
        //Debug.Log("Searching for enemy");

        // Search for gameobjects with the tag 'Enemy'
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Set the shortest distance to infinite 
        _shooting_StateMachine.shortestDistance = Mathf.Infinity;

        // Set the nearest enemy to null
        GameObject nearestEnemy = null;

        // Check for each enemy the distance and set the enemy with the shortest distance to be the nearest enemy
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(_shooting_StateMachine.turret.transform.position, enemy.transform.position);

            if (distanceToEnemy < _shooting_StateMachine.shortestDistance)
            {
                _shooting_StateMachine.shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && _shooting_StateMachine.shortestDistance <= _shooting_StateMachine._range)
        {
            //Debug.Log("Enemy found");
            _shooting_StateMachine.target = nearestEnemy;
            _shooting_StateMachine.SetState(new AttackTarget(_shooting_StateMachine));
            yield return new WaitForSeconds(0f);
        }
        else
        {
            //Debug.Log("No enemy found");
            _shooting_StateMachine.target = null;
            yield return new WaitForSeconds(0f);
        }
    }
}
