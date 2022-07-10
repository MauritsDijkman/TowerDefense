using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BasicEnemy))]

public class HealthBehaviour : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private float _health = 0f;
    private float _originalHealth = 0f;

    [SerializeField] private Image _healthbar = null;

    public void SetStartHealth(float pHealth)
    {
        _health = pHealth;
        _originalHealth = pHealth;
    }

    public void ExecuteBehaviour(float pDamage)
    {
        // Checking if the given damage is lower than 0. If so, set to 0.
        if (pDamage < 0)
        {
            pDamage = 0;
            throw new System.ArgumentOutOfRangeException("pDamage", "Only positive number is allowed for ExecuteBehaviour!");
            //Debug.Log("Given damage was below 0. Given damage has been made 0.");
        }

        // If the health is bigger than 0. If so, health minus damage
        if (_health > 0)
            _health -= pDamage;

        // If the health is lower than 0, set the health to 0
        if (_health < 0)
            _health = 0;

        // Update the health bar
        if (_healthbar != null)
            _healthbar.fillAmount = _health / _originalHealth;
    }

    // Return the health
    public float ReturnHealth()
    {
        return _health;
    }
}
