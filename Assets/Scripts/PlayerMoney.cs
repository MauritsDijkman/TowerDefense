using UnityEngine;
using TMPro;

public class PlayerMoney : MonoBehaviour
{
    [Header("Instance")]
    public static PlayerMoney instance;

    [Header("Money")]
    [SerializeField] private float startMoney = 90f;
    [HideInInspector] public float currentMoney;

    [Header("Text")]
    [SerializeField] private TMP_Text currentMoneyText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // If there is no text assigned, add it automaticly
        if (currentMoneyText == null)
            currentMoneyText = GetComponent<TMP_Text>();

        // Set current money to start money
        currentMoney = startMoney;

    }

    private void Update()
    {
        UpdateMoneyText();
    }


    private void UpdateMoneyText()
    {
        // Change the money text to the current money
        if (currentMoneyText != null)
            currentMoneyText.text = "<b>Money</b>\n" + "$" + currentMoney;
    }
}
