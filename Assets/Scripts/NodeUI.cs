using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [Header("Instance")]
    public static NodeUI instance;

    [Header("Other")]
    private Node target = null;
    private WaveSpawner waveSpawner = null;
    private TurretStats turretStats = null;
    private Canvas UI_Canvas = null;

    [Header("Upgrade")]
    [SerializeField] private Button upgradeButton = null;
    [SerializeField] private TMP_Text upgradeCostText = null;

    [Header("Sell")]
    [SerializeField] private Button sellButton = null;
    [SerializeField] private TMP_Text sellCostText = null;

    [Header("Stats Text")]
    [SerializeField] private TMP_Text levelText = null;
    [SerializeField] private TMP_Text damageText = null;
    [SerializeField] private TMP_Text rangeText = null;
    [SerializeField] private TMP_Text fireRateText = null;

    [Header("Node")]
    [HideInInspector] public Node selectedNode;

    [Header("Turret range")]
    private RadiusLine radiusLine;

    private void Awake()
    {
        // Find the canvs in the children of the gameobject
        UI_Canvas = GetComponentInChildren<Canvas>();

        // Find the wavespawner script in the scene
        waveSpawner = FindObjectOfType<WaveSpawner>();


        if (UI_Canvas == null)
            throw new System.Exception($"No UI_Canvas (Canvas) found in children components where NodeUI script is located.");
        if (waveSpawner == null)
            throw new System.Exception($"No waveSpawner (WaveSpawner) script found in scene where NodeUI script is located.");

        instance = this;
    }

    private void Start()
    {
        // Hide the canvas
        HideCanvas();
    }

    private void Update()
    {
        InteractibleHandler();
    }

    private void InteractibleHandler()
    {
        // Only run this code if the UI is active
        if (UI_Canvas.gameObject.activeSelf)
        {
            if (PlayerMoney.instance != null)
            {
                // If the player has not enough money to upgrade the turret, disable the upgrade button
                if (PlayerMoney.instance.currentMoney < turretStats._upgradeCost)
                {
                    if (upgradeButton.interactable == true)
                        upgradeButton.interactable = false;
                }
            }
            if (waveSpawner != null)
            {
                // If the current state of the wave spawner is not counting, disable the upgrade and sell button 
                if (waveSpawner.state != WaveSpawner.SpawnState.Counting)
                {
                    if (upgradeButton.interactable == true)
                        upgradeButton.interactable = false;

                    if (sellButton.interactable == true)
                        sellButton.interactable = false;
                }
                else
                    SetButtonText();    // Set the information of the buttons according to the selected turret
            }
        }
    }

    public void SetTarget(Node pTarget)
    {
        // Set the target to the given target
        target = pTarget;

        // Setting the current turretStats to the stats of the target turret
        if (target.currentTurretStats != null)
            turretStats = target.currentTurretStats;
        else
            throw new System.Exception($"CurrentTurretStats of the target node is null (error from NodeUI).");

        // Set the button text to the values of the current target
        SetButtonText();

        // Setting the stats with information about the selected turret
        SetInformationStats();

        // Setting the UI canvas active if it's not already
        if (!UI_Canvas.gameObject.activeSelf)
            UI_Canvas.gameObject.SetActive(true);
    }

    private void SetButtonText()
    {
        // If the target turret is not upgraded, enable the upgrade button and set the text to the upgrade cost
        if (!target.isUpgraded)
        {
            upgradeCostText.text = "$" + turretStats._upgradeCost;
            upgradeButton.interactable = true;
        }
        // If the turret is already upgraded, set the upgrade text to 'DONE' and disable the upgrade button
        else
        {
            upgradeCostText.text = "DONE";
            upgradeButton.interactable = false;
        }

        // Set the text of the sell cost to the cellcost of the selected turret
        sellCostText.text = "$" + turretStats._sellCost;

        // Set the sell button to interactive, if it's not already
        if (sellButton.interactable == false)
            sellButton.interactable = true;
    }

    public void HideCanvas()
    {
        // If the canvas is active, turn it off
        if (UI_Canvas.gameObject.activeSelf)
            UI_Canvas.gameObject.SetActive(false);
    }

    public void UpgradeTurret()
    {
        target.UpgradeTurret();                 // Call the upgrade function for the selected turret
        DeselectNode();   // Deselect the node when the turret has been upgraded
    }

    public void SellTurret()
    {
        target.SellTurret();                    // Call the sell function for the selected turret              
        DeselectNode();   // Deselect the node when the turret has been upgraded
    }

    private void SetInformationStats()
    {
        // Set the information of the turret to the text objects

        if (levelText != null)
            levelText.text = "Level: " + turretStats._level.ToString();
        else
            throw new System.Exception($"Level text on NodeUI script is null.");

        if (damageText != null)
            damageText.text = "Damage: " + turretStats._damage.ToString();
        else
            throw new System.Exception($"Damage text on NodeUI script is null.");

        if (rangeText != null)
            rangeText.text = "Range: " + turretStats._range.ToString();
        else
            throw new System.Exception($"Range text on NodeUI script is null.");

        if (fireRateText != null)
            fireRateText.text = "Fire rate: " + turretStats._fireRate.ToString();
        else
            throw new System.Exception($"FireRate text on NodeUI script is null.");
    }

    public void SelectNode(Node pNode)
    {
        // Deselect the node if it's this node
        if (selectedNode == pNode)
        {
            DeselectNode();
            return;
        }

        // Set the selected node to the given node
        selectedNode = pNode;

        // Set the target of the nodeUI to be the selected node
        SetTarget(selectedNode);

        // Remove the circle from the previous selected node if there was one
        if (radiusLine != null)
            radiusLine.HideCircle();

        // Set the radius line to the radius line of the selected node
        radiusLine = selectedNode.currentTurret.GetComponent<RadiusLine>();

        if (radiusLine != null)
            radiusLine.HideCircle();  // Remove the circle from the selected node

        // Enable the circle and give the radius as valu
        if (radiusLine != null)
            radiusLine.ShowCircle(pNode.currentTurret.GetComponent<TurretStats>()._range);

        //Debug.Log(selectedNode);
    }

    public void DeselectNode()
    {
        selectedNode = null;        // Set the selected node to null
        HideCanvas();              // Hide the node UI

        if (radiusLine != null)
            radiusLine.HideCircle();  // Remove the circle from the selected node
    }
}
