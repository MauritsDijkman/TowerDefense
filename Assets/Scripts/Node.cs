using UnityEngine;

public class Node : MonoBehaviour
{
    private SpriteRenderer _renderer = null;
    private CanvasTurret _canvasTurret = null;

    [Header("Color")]
    [SerializeField] private Color originalColor;
    [SerializeField] private Color hoverColor;

    [Header("Turret Information")]
    [HideInInspector] public GameObject currentTurret = null;
    [HideInInspector] public TurretStats currentTurretStats = null;
    private Turret turretToBuild = null;

    [HideInInspector] public bool isUpgraded = false;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _canvasTurret = FindObjectOfType<CanvasTurret>();

        if (_renderer == null)
            throw new System.Exception($"No _renderer (SpriteRender) found in components where Node script is located.");
        if (_canvasTurret == null)
            throw new System.Exception($"No _canvasTurret (CanvasTurret) found in components where Node script is located.");
    }

    private void Start()
    {
        // Setting the color of the material to the assigned original color 
        _renderer.material.color = originalColor;
    }

    private void OnMouseDown()
    {
        // If there is no input allowed, return
        if (GameManager.instance != null)
            if (!GameManager.instance.inputEnabled)
                return;

        // If there is a turret located on the node and there is no turret on the mouse, select the node (give information about located turret) and return
        if (currentTurret != null & !_canvasTurret.turretOnMouse)
        {
            if (NodeUI.instance != null)
            {
                NodeUI.instance.SelectNode(this);
                return;
            }
        }

        // If there is no turret located on the mouse, return
        if (!_canvasTurret.turretOnMouse)
            return;

        // If there is already a turret located on the node, return
        if (currentTurret != null)
        {
            Debug.Log("Can't build here! There is already a turret located here.");
            return;
        }

        // Build the turret
        BuildTurret();
    }

    private void BuildTurret()
    {
        // Set the gameobject to the turretModel
        turretToBuild = _canvasTurret.turretToBuild;

        // Spawning the turret
        currentTurret = (GameObject)Instantiate(turretToBuild.TurretModel, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.01f), transform.rotation);
        currentTurret.transform.parent = GameObject.Find("Turrets").transform;

        // Enabling the target shooting and setting up the stats
        currentTurret.AddComponent<TurretStats>().SetValues(turretToBuild.TurretType, turretToBuild.Level, turretToBuild.Damage, turretToBuild.Range, turretToBuild.FireRate, turretToBuild.BuyCost, turretToBuild.UpgradeCost, turretToBuild.SellCost, turretToBuild.UpgradedTurretModel);
        currentTurret.GetComponent<Shooting_StateMachine>().enabled = true;
        currentTurretStats = currentTurret.GetComponent<TurretStats>();

        // Removing the costs of the turret from the current money
        if (PlayerMoney.instance != null)
            PlayerMoney.instance.currentMoney -= turretToBuild.BuyCost;

        // Disabeling the renderer of the node
        _renderer.enabled = false;

        // Destroying the turret model on the mouse
        FindObjectOfType<FollowMouse>().DestroyTurretOnMouse();
    }

    private void OnMouseEnter()
    {
        // If there is no input allowed, return
        if (GameManager.instance != null)
            if (!GameManager.instance.inputEnabled)
                return;

        // If there is a turret located on the node and there is no turret on the mouse, hightlight the node and return
        if (currentTurret != null & !_canvasTurret.turretOnMouse)
        {
            _renderer.enabled = true;
            _renderer.material.color = hoverColor;
            return;
        }

        // If there is already a turret located on the node, return
        if (currentTurret != null)
            return;

        // If there is no turret located on the mouse, return
        if (!_canvasTurret.turretOnMouse)
            return;

        // Hightlight the node
        _renderer.enabled = true;
        _renderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        // If there is no input allowed, return
        if (GameManager.instance != null)
            if (!GameManager.instance.inputEnabled)
                return;

        // Setting the color of the material to the original color
        _renderer.material.color = originalColor;
    }

    public void UpgradeTurret()
    {
        // Setting the stats of the current turret to the current turret stats, so that it can be used later in the script
        TurretStats _currentTurretStats = currentTurret.GetComponent<TurretStats>();

        if (_currentTurretStats == null)
            throw new System.Exception($"No _currentTurretStats (TurretStats) found in components where HealthChecker script is located.");

        if (GameManager.instance == null)
            throw new System.Exception($"BuildManager is null (error from Node.cs).");


        //  If the turret is already upgraded, return
        if (isUpgraded)
        {
            Debug.Log("Turret is already upgraded!");
            return;
        }

        // If the player doesn't have enough money, return
        if (PlayerMoney.instance != null)
        {
            if (PlayerMoney.instance.currentMoney < _currentTurretStats._upgradeCost)
            {
                Debug.Log("Not enough money to upgrade the turret!");
                return;
            }
        }

        // Destroy the current turret that is located on the node
        Destroy(currentTurret);

        // Remove the upgrade cost from the current money
        if (PlayerMoney.instance != null)
            PlayerMoney.instance.currentMoney -= _currentTurretStats._upgradeCost;

        // Spawn the upgraded turret and set it to to 'Turrets' list
        GameObject upgradedTurret = (GameObject)Instantiate(_currentTurretStats._upgradedTurretModel, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.01f), transform.rotation);
        upgradedTurret.transform.parent = GameObject.Find("Turrets").transform;

        //Debug.Log(currentTurretStats._type);

        // Add the TurretStats script to the upgraded turret gameobject and set better values according to the overal GameMaster
        if (currentTurretStats != null && GameManager.instance != null)
        {
            if (currentTurretStats._type == "NormalTurret")
                upgradedTurret.AddComponent<TurretStats>().SetValues("UpgradedNormalTuret",
                    _currentTurretStats._level + 1,
                    _currentTurretStats._damage + GameManager.instance._normalPlusDamage,
                    _currentTurretStats._range + GameManager.instance._normalPlusRange,
                    _currentTurretStats._fireRate + GameManager.instance._normalPlusFireRate,
                    _currentTurretStats._buyCost,
                    _currentTurretStats._upgradeCost,
                    _currentTurretStats._sellCost + GameManager.instance._normalPlusSellCost,
                    null);
            else if (currentTurretStats._type == "HeavyTurret")
                upgradedTurret.AddComponent<TurretStats>().SetValues("UpgradedHeavyTuret",
                    _currentTurretStats._level + 1,
                    _currentTurretStats._damage + GameManager.instance._heavyPlusDamage,
                    _currentTurretStats._range + GameManager.instance._heavyPlusRange,
                    _currentTurretStats._fireRate + GameManager.instance._heavyPlusFireRate,
                    _currentTurretStats._buyCost,
                    _currentTurretStats._upgradeCost,
                    _currentTurretStats._sellCost + GameManager.instance._heavyPlusSellCost,
                    null);
            else if (currentTurretStats._type == "DebuffTurret")
                upgradedTurret.AddComponent<TurretStats>().SetValues("UpgradedDebuffTuret",
                    _currentTurretStats._level + 1,
                    _currentTurretStats._damage + GameManager.instance._debuffPlusDamage,
                    _currentTurretStats._range + GameManager.instance._debuffPlusRange,
                    _currentTurretStats._fireRate + GameManager.instance._debuffPlusFireRate,
                    _currentTurretStats._buyCost,
                    _currentTurretStats._upgradeCost,
                    _currentTurretStats._sellCost + GameManager.instance._debuffPlusSellCost,
                    null);
        }

        // Setting the current turret to the upgraded turret
        currentTurret = upgradedTurret;
        currentTurretStats = currentTurret.GetComponent<TurretStats>();

        // Set isUpgraded to true
        isUpgraded = true;

        //Debug.Log("Turret upgraded!");
    }

    public void SellTurret()
    {
        // Add the sell cost to the current money
        if (PlayerMoney.instance != null)
            PlayerMoney.instance.currentMoney += currentTurretStats._sellCost;

        Destroy(currentTurret);     // Destroy the current turret
        currentTurret = null;       // Set the current turret to null
        isUpgraded = false;         // Set isUpgraded to false
    }
}
