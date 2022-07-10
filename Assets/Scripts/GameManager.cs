using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Instance")]
    public static GameManager instance;
    [NonSerialized] public bool inputEnabled;

    [Header("Text")]
    [SerializeField] private TMP_Text normalTurretCostText = null;
    [SerializeField] private TMP_Text heavyTurretCostText = null;
    [SerializeField] private TMP_Text debuffTurretCostText = null;

    [Header("UI")]
    [SerializeField] private GameObject worthUI = null;

    [Header("Panels")]
    [SerializeField] private GameObject gameOverPanel = null;
    [SerializeField] private GameObject victoryPanel = null;

    [Header("Turrets")]
    public NormalTurretFactory normalTurretStats = null;
    public HeavyTurretFactory heavyTurretStats = null;
    public DebuffTurretFactory debuffTurretStats = null;

    [Header("Upgrade values - Normal tower")]
    [SerializeField] public float _normalPlusDamage = 1f;
    [SerializeField] public float _normalPlusRange = 1f;
    [SerializeField] public float _normalPlusFireRate = 1f;
    [SerializeField] public int _normalPlusSellCost = 50;

    [Header("Upgrade values - Heavy tower")]
    [SerializeField] public float _heavyPlusDamage = 0.5f;
    [SerializeField] public float _heavyPlusRange = 1f;
    [SerializeField] public float _heavyPlusFireRate = 0.5f;
    [SerializeField] public int _heavyPlusSellCost = 50;

    [Header("Upgrade values - Debuff tower")]
    [SerializeField] public float _debuffPlusDamage = 1f;
    [SerializeField] public float _debuffPlusRange = 1f;
    [SerializeField] public float _debuffPlusFireRate = 1f;
    [SerializeField] public int _debuffPlusSellCost = 50;


    private void Awake()
    {
        instance = this;        // Set the instance to this class
        inputEnabled = true;    // Enable input
    }

    private void Start()
    {
        // Turn the game over panal off if it was on
        if (gameOverPanel.activeSelf)
            gameOverPanel.SetActive(false);

        // Set the costs for the turrets / towers
        normalTurretCostText.text = "Costs: $" + normalTurretStats.ReturnBuyCost();
        heavyTurretCostText.text = "Costs: $" + heavyTurretStats.ReturnBuyCost();
        debuffTurretCostText.text = "Costs: $" + debuffTurretStats.ReturnBuyCost();
    }

    private void Update()
    {
        HandleQuitButton();
    }

    private void HandleQuitButton()
    {
        // Close the game is 'Escape' is pressed
        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
    }

    public void GameOver()
    {
        // Disable the input and freeze the time
        inputEnabled = false;
        Time.timeScale = 0f;


        // Enable the game over panel, if it was not enabled
        if (!gameOverPanel.activeSelf)
            gameOverPanel.SetActive(true);
    }

    public void Victory()
    {
        // Disable the input and freeze the time
        inputEnabled = false;
        Time.timeScale = 0f;

        // Enable the game over panel, if it was not enabled
        if (!victoryPanel.activeSelf)
            victoryPanel.SetActive(true);
    }

    public GameObject GetWorthUI()
    {
        // Return the worth UI so that it can be used for spawning
        return worthUI;
    }
}
