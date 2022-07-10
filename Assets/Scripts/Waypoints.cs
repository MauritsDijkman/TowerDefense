using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // Static so that it can be accessed easily 
    public static Transform[] waypoints;

    // Adding all transform of all waypoints to the list with waypoints
    private void Awake()
    {
        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
