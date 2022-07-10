using UnityEngine;

[RequireComponent(typeof(BasicEnemy))]

public class MovementBehaviour : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float _movementSpeed = 0f;
    [SerializeField] private float _rotationSpeed = 0f;

    [Header("Transform")]
    private Transform target = null;
    private Transform objectToRotate = null;

    [Header("Waypoint")]
    public int waypointIndex = 0;


    public void SetSpeed(float pMovementSpeed)
    {
        // Set the movement speed to the given speed and the rotation speed to be three times quicker than the movement speed
        _movementSpeed = pMovementSpeed;
        _rotationSpeed = pMovementSpeed * 3;
    }

    private void Start()
    {
        // Set the object to rotate to the rotate function
        if (gameObject.transform.Find("EnemySprite") != null)
            objectToRotate = gameObject.transform.Find("EnemySprite");

        // Set the target to be the first waypoint
        if (Waypoints.waypoints != null)
            target = Waypoints.waypoints[0];
    }

    public void ExecuteBehaviour()
    {
        MoveToTarget();
        FaceTarget();

        // If the enemy is close to the waypoint, go to the next one
        if (Vector3.Distance(objectToRotate.position, target.position) <= 0.05f)
            GetNextWaypoint();
    }

    private void MoveToTarget()
    {
        // Set the direction and move to the direction
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * _movementSpeed * Time.deltaTime, Space.World);
    }

    private void FaceTarget()
    {
        // Calculate the rotation and rotate to always face the waypoint
        Vector3 dir = target.position - transform.position;
        Vector3 rotatedVectorDir = Quaternion.Euler(0, 0, 90) * dir;
        Quaternion lookRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorDir);
        Quaternion rotation = Quaternion.Lerp(objectToRotate.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        objectToRotate.rotation = rotation;
    }

    private void GetNextWaypoint()
    {
        // Checks if the list has reached it's end
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            waypointIndex++;
            return;
        }

        // Go to the next waypoint and assign the target
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    public int ReturnWaypointIndex()
    {
        return waypointIndex;
    }
}
