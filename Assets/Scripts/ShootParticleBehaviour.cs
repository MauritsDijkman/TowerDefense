using UnityEngine;

public class ShootParticleBehaviour : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroy = 0.05f;

    private void Update()
    {
        // If no input is allowed, return
        if (GameManager.instance != null)
            if (!GameManager.instance.inputEnabled)
                return;

        // If the time has expired, destroy the gameobject (particle)
        if (timeBeforeDestroy <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        // Decrease the time with real time
        timeBeforeDestroy -= Time.deltaTime;
    }
}
