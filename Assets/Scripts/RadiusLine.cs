using UnityEngine;

public class RadiusLine : MonoBehaviour
{
    private LineRenderer circleRenderer = null;

    private void Awake()
    {
        circleRenderer = GetComponent<LineRenderer>();

        if (circleRenderer == null)
            throw new System.Exception($"No circleRenderer (LineRenderer) found in components where RadiusLine script is located.");
    }

    public void ShowCircle(float radius)
    {
        // Draw the circle with a smoothness of 30
        DrawCircle(30, radius);
    }

    public void HideCircle()
    {
        // Turn the circle rendere off it's on
        if (circleRenderer.enabled == true)
            circleRenderer.enabled = false;
    }

    // Calculate the position and scale of the circle renderer
    private void DrawCircle(int steps, float radius)
    {
        circleRenderer.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x, y, gameObject.transform.position.z);

            circleRenderer.SetPosition(currentStep, currentPosition);

            circleRenderer.enabled = true;
        }
    }
}
