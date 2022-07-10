using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerCastleHP : MonoBehaviour
{
    [Header("Instance")]
    public static PlayerCastleHP instance;

    [Header("Text")]
    [SerializeField] private TMP_Text castleHPText;

    [Header("Values")]
    [SerializeField] public float startCastleHP = 100f;
    [HideInInspector] public float currentCastleHP = 0f;

    [Header("UI")]
    [SerializeField] private Image castleHPFillImage;
    [SerializeField] private Gradient castleHPGradient;
    [SerializeField] private Slider castleHPSlider;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Set the current HP to the start HP
        currentCastleHP = startCastleHP;
    }

    private void Update()
    {
        UpdateCastleHP();
    }

    private void UpdateCastleHP()
    {
        // If the current HP is bigger than the start HP, set the current HP to the start HP
        if (currentCastleHP > startCastleHP)
            currentCastleHP = startCastleHP;

        // If the current HP is less than or equal to 0, set the current HP to 0 and call the GameOver function
        if (currentCastleHP <= 0)
        {
            currentCastleHP = 0;

            if (GameManager.instance != null)
                GameManager.instance.GameOver();
        }

        // Set the HP text to the current HP
        if (castleHPText != null)
            castleHPText.text = currentCastleHP.ToString();

        // Set the slider value to the current HP
        if (castleHPSlider != null)
            castleHPSlider.value = currentCastleHP;

        // Set the gradient of the slider to the normalized value of the current HP
        if (castleHPFillImage != null)
            castleHPFillImage.color = castleHPGradient.Evaluate(castleHPSlider.normalizedValue);
    }
}
