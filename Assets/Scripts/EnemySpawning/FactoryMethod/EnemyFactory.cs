using UnityEngine;

public abstract class EnemyFactory : MonoBehaviour
{
    /// <summary>
    /// This class is the general enemy factory and has the abstract Enemy class
    /// </summary>
    /// 

    public abstract Enemy GetEnemy();
}
