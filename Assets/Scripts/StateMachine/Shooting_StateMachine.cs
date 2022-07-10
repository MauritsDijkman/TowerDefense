using UnityEngine;

/// <summary>
/// This class has an implementation of the StateMachine class
/// </summary>

public class Shooting_StateMachine : StateMachine
{
    public GameObject turret = null;
    public GameObject target = null;
    public float fireCountdown = 0f;

    [Header("Rotationspeed")]
    public float rotationSpeed = 10f;

    [Header("Object")]
    public Transform partToRotate = null;
    public GameObject bulletPrefab = null;
    public Transform firePoint = null;
    public GameObject shootParticle = null;

    [Header("Values")]
    public float _range = 0;
    public float _fireRate = 0;
    public float _damage = 0;

    [Header("Distance")]
    public float shortestDistance;

    private void Start()
    {
        SetState(new SettingValues(this));
        //Debug.Log("Start function of turret shooting behaviour is called");
    }

    private void Update()
    {
        // If no input is allowed, return
        if (GameManager.instance != null)
            if (!GameManager.instance.inputEnabled)
                return;

        CheckForValueChange();

        // If target is null, search for a new target and return
        if (target == null)
        {
            //Debug.Log($"State: {_state} || State string: {_state.ToString()}");

            SetState(new SearchTarget(this));
            StartCoroutine(_state.Search());
            return;
        }

        // If the enemy is outside of the range of the turret, set target to null and return
        if (Vector3.Distance(turret.transform.position, target.transform.position) > _range)
        {
            target = null;
            return;
        }

        LookAtTarget();
        ShootAtTarget();

        //Debug.Log("Update function of turret shooting behaviour is called");
    }

    private void CheckForValueChange()
    {
        // If a value is not the same as a value is the stats of the turret, update the stats
        if (_range != turret.GetComponent<TurretStats>()._range || _fireRate != turret.GetComponent<TurretStats>().
            _fireRate || _damage != turret.GetComponent<TurretStats>()._damage)
            SetState(new SettingValues(this));
    }

    private void LookAtTarget()
    {
        // If part to rotate is not null, rotate to always face enemy
        if (partToRotate != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            Vector3 rotatedVectorDir = Quaternion.Euler(0, 0, 0) * dir;
            Quaternion lookRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorDir);
            Quaternion rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            partToRotate.rotation = rotation;
        }
    }

    private void ShootAtTarget()
    {
        // If fire countdown is less then or equal to 0, shoot and set the countdown back to fire rate
        if (fireCountdown <= 0f)
        {
            fireCountdown = 1f / _fireRate;
            //Debug.Log("New fire countdown: " + fireCountdown);
            StartCoroutine(_state.Attack());
        }

        // Decrease the countdown
        fireCountdown -= Time.deltaTime;

        //Debug.Log("Fire countdown: " + fireCountdown);
    }

    // Function that can be called by the states
    public GameObject SpawnObject(GameObject pObject, Transform pPosition, Transform pRotation)
    {
        return (GameObject)Instantiate(pObject, pPosition.position, pRotation.rotation);
    }
}
