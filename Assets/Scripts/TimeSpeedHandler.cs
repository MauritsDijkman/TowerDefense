using UnityEngine;
using TMPro;

public class TimeSpeedHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText = null;

    private void Start()
    {
        if (buttonText == null)
            throw new System.Exception($"No buttonText (TMP_Text) is assigned to the SpeedController script.");

        // Set the speed and the text to be 1
        buttonText.text = "1.0x";
        Time.timeScale = 1f;
    }

    public void ChangeSpeed()
    {
        // Asjust the speed and text according to the text
        if (buttonText.text == "0.5x")
            ChangeSpeedAndText("1.0x", 1.0f);
        else if (buttonText.text == "1.0x")
            ChangeSpeedAndText("2.0x", 2.0f);
        else if (buttonText.text == "2.0x")
            ChangeSpeedAndText("3.0x", 3.0f);
        else if (buttonText.text == "3.0x")
            ChangeSpeedAndText("0.5x", 0.5f);
    }

    private void ChangeSpeedAndText(string text, float time)
    {
        // Sets the text and timeScale to the given values
        buttonText.text = text;
        Time.timeScale = time;
    }
}
