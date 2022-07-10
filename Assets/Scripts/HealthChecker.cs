using UnityEngine;
using TMPro;
using System;

public class HealthChecker : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text informationText;

    [Header("Enemy factory stats")]
    [SerializeField] private NormalEnemyFactory normalEnemyStats;
    [SerializeField] private FastEnemyFactory fastEnemyStats;
    [SerializeField] private TankEnemyFactory tankEnemyStats;

    private void Update()
    {
        CalculateAmount();
    }

    private void CalculateAmount()
    {
        if (PlayerCastleHP.instance != null)
        {
            // Calculate how many times the current castle HP can be devided by the damage of the normal enemy
            float normal = PlayerCastleHP.instance.currentCastleHP / normalEnemyStats.ReturnStartValue("damage");
            int normalRoundedUp = (int)Math.Ceiling(normal);    // Round the number from the previous calculation up to a whole number

            // Calculate how many times the current castle HP can be devided by the damage of the fast enemy
            float fast = PlayerCastleHP.instance.currentCastleHP / fastEnemyStats.ReturnStartValue("damage");
            int fastRoundedUp = (int)Math.Ceiling(fast);        // Round the number from the previous calculation up to a whole number

            // Calculate how many times the current castle HP can be devided by the damage of the tank enemy
            float tank = PlayerCastleHP.instance.currentCastleHP / tankEnemyStats.ReturnStartValue("damage");
            int tankRoundedUp = (int)Math.Ceiling(tank);        // Round the number from the previous calculation up to a whole number

            // Setting the UI text to the calculated information 
            if (informationText != null)
                informationText.text = $"Normal: {normalRoundedUp}\nFast: {fastRoundedUp}\nTank: {tankRoundedUp}";
            else
                throw new System.Exception($"No informationText (TMP_Text) found in components where HealthChecker script is located.");

            //Debug.Log($"Normal: {normalRoundedUp} || Fast: {fastRoundedUp} || Tank: {tankRoundedUp}");
        }
        else
            throw new System.Exception($"No instance of BuildManager found in scene where HealthChecker script is located.");
    }
}
