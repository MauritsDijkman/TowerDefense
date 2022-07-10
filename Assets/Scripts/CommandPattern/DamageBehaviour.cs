using UnityEngine;

public class DamageBehaviour : ICommand
{
    [Header("Value")]
    [SerializeField] private float _damage = 0f;


    public DamageBehaviour(float pDamage)
    {
        if (pDamage < 0)
            throw new System.ArgumentOutOfRangeException("pDamage", "Only positive number is allowed for DamageBehaviour!");

        // Set the local damage to the given damage
        _damage = pDamage;
    }

    // Do damage to the castle
    public void Execute()
    {
        if (PlayerCastleHP.instance != null)
            PlayerCastleHP.instance.currentCastleHP -= _damage;
    }

    // Undo the damage to the castle
    public void Undo()
    {
        if (PlayerCastleHP.instance != null)
            PlayerCastleHP.instance.currentCastleHP += _damage;
    }
}
