using UnityEngine;

public class DebuffToggle : ICommand
{
    private GameObject _target = null;

    private BasicEnemy enemyStats = null;

    private float originalSpeed = 0f;
    private Color32 originalColor = new Color32(255, 255, 255, 255);
    private SpriteRenderer spriteRenderer = null;


    public DebuffToggle(GameObject pTarget)
    {
        // Setting the target to the given target
        _target = pTarget;
    }

    public void Execute()
    {
        //Debug.Log("Execute of the DebuffToggle is called");

        SetOriginalValues();
        SetDebuff();
    }

    public void Undo()
    {
        //Debug.Log("Undo of the DebuffToggle is called");

        // Setting the values back to their original state
        if (enemyStats != null)
            enemyStats.SetSpeed(originalSpeed);
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
    }

    // Setting the values to be used later
    private void SetOriginalValues()
    {
        spriteRenderer = _target.GetComponentInChildren<SpriteRenderer>();

        enemyStats = _target.GetComponent<BasicEnemy>();

        originalSpeed = enemyStats.GetSpeed();
        originalColor = spriteRenderer.color;
    }

    private void SetDebuff()
    {
        // Setting the speed of the enemy to be half of the original speed
        if (enemyStats != null)
            enemyStats.SetSpeed(enemyStats.GetSpeed() * 0.5f);

        // Setting the sprite color to have a blue filter on it (simulates frozen state)
        if (spriteRenderer != null)
            spriteRenderer.color = new Color32(155, 155, 255, 255);
    }
}
