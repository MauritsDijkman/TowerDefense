using UnityEngine;

/// <summary>
/// This abstract class contains all basic values of all enemy types
/// </summary>

public abstract class Enemy
{
    public abstract GameObject EnemyModel { get; set; }
    public abstract string EnemyType { get; }
    public abstract int Level { get; set; }
    public abstract float Health { get; set; }
    public abstract float Speed { get; set; }
    public abstract float Worth { get; set; }
    public abstract float Damage { get; set; }
}
