using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text enemyText;
    [HideInInspector] public int enemyNumber;

    private void Update()
    {
        HandleText();
    }

    private void HandleText()
    {
        // Set the number of enemies to the childcount of the gameobject
        enemyNumber = transform.childCount;

        // Set the number of enemies text to the current number of enemies
        if (enemyText != null)
            enemyText.text = "<b>Enemies</b>\n" + enemyNumber.ToString();
        else
            throw new System.Exception("No enemyText (TMP_Text) attached to the EnemyCounter script.");
    }
}
