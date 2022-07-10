using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyInformationHolder
    {
        public string Name;
        [Range(0f, 100f)] public int changeOfSpawning = 100;
        [HideInInspector] public int _weight;
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public EnemyInformationHolder[] enemies;
    }

    [HideInInspector] public enum SpawnState { Spawning, Counting, Waiting };
    [HideInInspector] public SpawnState state = SpawnState.Counting;

    [Header("Waves Information")]
    [SerializeField] private Wave[] waves;
    private int nextWave = 0;

    [Header("Text")]
    [SerializeField] private TMP_Text waveCounterText = null;
    [SerializeField] private TMP_Text timerText = null;

    [Header("Time")]
    [SerializeField] private float timeBetweenEachWave = 10f;
    [SerializeField] private float timeBetweenEachEnemy = 0.4f;

    private float waveCountdown;
    private float searchCountdown = 1f;

    private EnemyCounter enemyCounter = null;
    private EnemySpawner enemySpawner = null;

    private int accumulatedWeights;
    private System.Random rand = new System.Random();

    private void Awake()
    {
        enemyCounter = FindObjectOfType<EnemyCounter>();
        enemySpawner = FindObjectOfType<EnemySpawner>();

        CalculateWeights();
    }

    private void Start()
    {
        waveCountdown = timeBetweenEachWave;    // Setting the wave count dowm timer to the time between waves
        SetWaveCounterText(0);                  // Setting the wave counter text to the 0 with the total of all waves
    }

    private void Update()
    {
        // Checking if the current state is the wait state
        // If all enemies are dead, the wave is completed. If not, return.
        if (state == SpawnState.Waiting)
        {
            if (!EnemiesAlive())
                WaveCompleted();
            else
                return;
        }

        // Checking if the wave countdown timer is below zero and if the current state is not the spawning state
        // If not, decrease the timer
        if (waveCountdown <= 0 && state != SpawnState.Spawning)
            StartCoroutine(SpawnWave(waves[nextWave]));
        else
            waveCountdown -= Time.deltaTime;

        // If the current state is the counting state, update the next wave timer countdown to the current countdowm time
        if (state == SpawnState.Counting)
            timerText.text = "<b>Next wave</b>\n" + waveCountdown.ToString("F0");
        else
            timerText.text = "<b>Next wave</b>\n" + "Waiting";
    }

    // Setting the weight of every enemy to the change of spawning
    private void CalculateWeights()
    {
        accumulatedWeights = 0;

        foreach (EnemyInformationHolder enemy in waves[nextWave].enemies)
        {
            accumulatedWeights += enemy.changeOfSpawning;
            enemy._weight = accumulatedWeights;
        }
    }

    private void WaveCompleted()
    {
        //Debug.Log("Wave completed");

        // Setting the current state to the counting state
        state = SpawnState.Counting;
        // Setting the wave countdowm timer to the time between waves
        waveCountdown = timeBetweenEachWave;

        // Checking if the end of the waves is already there. If so, start again with the first wave and return.
        // If not, spawn the netxt wave and calculate the spawn change of each enemy
        if (nextWave + 1 > waves.Length - 1)
        {
            //nextWave = 0;
            //Debug.Log("Completed all waves - Now looping");
            GameManager.instance.Victory();
            return;
        }
        else
        {
            nextWave++;
            CalculateWeights();
        }
    }

    private bool EnemiesAlive()
    {
        // Decreasing the search count down timer with real time
        // Search countdown is 1 seconds, so every second this function checks if all enemies are dead
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;

            if (enemyCounter.enemyNumber <= 0)
                return false;
        }

        return true;
    }

    private void SetWaveCounterText(int waveNumber)
    {
        waveCounterText.text = "<b>Wave</b> \n" + waveNumber + " / " + waves.Length.ToString();
    }

    private IEnumerator SpawnWave(Wave _wave)
    {
        //Debug.Log("Spawning wave");

        //waveCounterText.text = "<b>Wave</b> \n" + (nextWave + 1) + "/" + waves.Length.ToString();
        SetWaveCounterText(nextWave + 1);

        // Setting the current state to the spawning state
        state = SpawnState.Spawning;

        for (int i = 0; i < _wave.enemyCount; i++)
        {
            // Spawn the number of enemies with a given time between the spawns
            SpawnRandomEnemy();
            yield return new WaitForSeconds(timeBetweenEachEnemy);
        }

        // Setting the spawn state to waiting
        state = SpawnState.Waiting;

        yield break;
    }

    private void SpawnRandomEnemy()
    {
        // Assign a random enemy
        EnemyInformationHolder randomEnemy = waves[nextWave].enemies[GetRandomEnemyIndex()];

        // Spawn the enemy that is given
        if (randomEnemy.Name.ToLower() == "regular")
            enemySpawner.SpawnNormalEnemy();
        else if (randomEnemy.Name.ToLower() == "fast")
            enemySpawner.SpawnFastEnemy();
        else if (randomEnemy.Name.ToLower() == "tank")
            enemySpawner.SpawnTankEnemy();
        else if (randomEnemy.Name.ToLower() == "boss")
            enemySpawner.SpawnBossEnemy();
    }

    // Calculate and return a random enemy (calculates with the spawning change)
    private int GetRandomEnemyIndex()
    {
        int r = (int)(rand.NextDouble() * accumulatedWeights);

        for (int i = 0; i < waves[nextWave].enemies.Length; i++)
            if (waves[nextWave].enemies[i]._weight >= r)
                return i;

        return 0;
    }
}
