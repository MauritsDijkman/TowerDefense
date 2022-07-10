using UnityEngine;

/// <summary>
/// This class handles the spawning of all enemies
/// </summary>

public class EnemySpawner : MonoBehaviour
{
    private EnemyFactory factory = null;

    [Header("Factories")]
    [SerializeField] private NormalEnemyFactory normalFactory;
    [SerializeField] private FastEnemyFactory fastFactory;
    [SerializeField] private TankEnemyFactory tankFactory;
    [SerializeField] private BossEnemyFactory bossFactory;

    [Header("Spawnpoint")]
    [SerializeField] private GameObject spawnPoint = null;

    private void Start()
    {
        // Set the switch state to do nothing
        SwitchEnemy("start");
    }

    private void SpawnEnemyInScene(Enemy _enemy)
    {
        if (_enemy != null && spawnPoint != null)
        {
            // Spawn the enemy on the given spawnposition and place it in the 'Enemies' list
            GameObject enemyModel = Instantiate(_enemy.EnemyModel, spawnPoint.transform.position, spawnPoint.transform.rotation);

            if (GameObject.Find("Enemies") != null)
                enemyModel.transform.parent = GameObject.Find("Enemies").transform;

            BasicEnemy enemyStats = enemyModel.GetComponent<BasicEnemy>();
            if (enemyStats != null)
            {
                enemyStats.SetLevel(_enemy.Level);
                enemyStats.SetHealth(_enemy.Health);
                enemyStats.SetSpeed(_enemy.Speed);
                enemyStats.SetWorth(_enemy.Worth);
                enemyStats.SetDamage(_enemy.Damage);
            }
        }
        else
            throw new System.Exception("No _enemy (Enemy) and/or _spawnPoint (GameObject) given to the SpawnEnemyInScene function in the EnemySpawner script.");
    }

    public void SpawnNormalEnemy()
    {
        SwitchEnemy("regular");
        SpawnEnemyInScene(factory.GetEnemy());
        SwitchEnemy("start");
    }

    public void SpawnFastEnemy()
    {
        SwitchEnemy("fast");
        SpawnEnemyInScene(factory.GetEnemy());
        SwitchEnemy("start");
    }

    public void SpawnTankEnemy()
    {
        SwitchEnemy("tank");
        SpawnEnemyInScene(factory.GetEnemy());
        SwitchEnemy("start");
    }

    public void SpawnBossEnemy()
    {
        SwitchEnemy("boss");
        SpawnEnemyInScene(factory.GetEnemy());
        SwitchEnemy("start");
    }

    private void SwitchEnemy(string _enemy)
    {
        switch (_enemy.ToLower())
        {
            case "start":
                factory = null;
                break;

            case "regular":
                factory = normalFactory;
                break;

            case "fast":
                factory = fastFactory;
                break;

            case "tank":
                factory = tankFactory;
                break;

            case "boss":
                factory = bossFactory;
                break;
        }
    }
}
