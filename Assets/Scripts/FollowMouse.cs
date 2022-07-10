using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private CanvasTurret canvasTurret;
    private Vector3 mousePosition;

    private void Awake()
    {
        canvasTurret = FindObjectOfType<CanvasTurret>();
    }

    private void Update()
    {
        // If there is not input allowed, return
        if (GameManager.instance != null)
            if (!GameManager.instance.inputEnabled)
                return;

        // Setting the mouse position
        UpdateMousePosition();

        // Setting the position of the object where the script is located to the x and y position of the mouse
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        // If the right mouse button is pressed, destroy the turret that is located on the mouse
        if (Input.GetMouseButtonDown(1))
            DestroyTurretOnMouse();
    }

    private void UpdateMousePosition()
    {
        // Setting the mouse position
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void DestroyTurretOnMouse()
    {
        // Set the boolean that handles the turret on mouse state to false
        if (canvasTurret != null)
            canvasTurret.turretOnMouse = false;
        else
            throw new System.Exception($"No CanvasTurret script found in the scene according to the FollowMouse script.");

        // Destroy gameObject where is script is located on
        Destroy(gameObject);

        //Debug.Log("Turret on mouse is destroyed");
    }
}
