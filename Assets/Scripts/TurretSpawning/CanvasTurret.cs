using UnityEngine;
using System;
using UnityEngine.UI;

public class CanvasTurret : MonoBehaviour
{
    [NonSerialized] public bool turretOnMouse = false;

    private TurretSpawner turretSpawner = null;
    public Turret turretToBuild = null;
    private WaveSpawner waveSpawner = null;

    [Header("Turrets")]
    [SerializeField] private Button normalTurret = null;
    [SerializeField] private Button heavyTurret = null;
    [SerializeField] private Button debuffTurret = null;

    private void Awake()
    {
        turretSpawner = FindObjectOfType<TurretSpawner>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    private void Start()
    {
        //if (turretSpawner == null)
        //    throw new System.Exception("No TurretSpawner found in scene where CanvasTurret.cs is located.");
        //if (PlayerMoney.instance == null)
        //    throw new System.Exception("PlayerMoney.instance is null in scene where CanvasTurret.cs is located.");
    }

    private void Update()
    {
        if (GameManager.instance != null && waveSpawner != null)
            HandleTurretInteractibility();
        //else if (waveSpawner == null)
        //    throw new System.Exception("No WaveSpawner found in scene where CanvasTurret.cs is located.");
        //else if (waveSpawner == null)
        //    throw new System.Exception("Buildmanager.instance is null in scene where CanvasTurret.cs is located.");
    }

    public void SpawnNormalTurret()
    {
        // If there is a node selected, deselect the node
        if (NodeUI.instance != null)
            if (NodeUI.instance.selectedNode != null)
                NodeUI.instance.DeselectNode();

        // If there is no turret on the mouse, spawn a normal turret and set the stats to the turret to build
        if (!turretOnMouse)
        {
            turretToBuild = turretSpawner.GetNormalTurret();
            turretOnMouse = true;
        }
    }

    public void SpawnHeavyTurret()
    {
        // If there is a node selected, deselect the node
        if (NodeUI.instance != null)
            if (NodeUI.instance.selectedNode != null)
                NodeUI.instance.DeselectNode();

        // If there is no turret on the mouse, spawn a heavy turret and set the stats to the turret to build
        if (!turretOnMouse)
        {
            turretToBuild = turretSpawner.GetHeavyTurret();
            turretOnMouse = true;
        }
    }

    public void SpawnDebuffTurret()
    {
        // If there is a node selected, deselect the node
        if (NodeUI.instance != null)
            if (NodeUI.instance.selectedNode != null)
                NodeUI.instance.DeselectNode();

        // If there is no turret on the mouse, spawn a debuff turret and set the stats to the turret to build
        if (!turretOnMouse)
        {
            turretToBuild = turretSpawner.GetDebuffTurret();
            turretOnMouse = true;
        }
    }

    private void HandleTurretInteractibility()
    {
        // Changing normal turret interactability according to money and spawn state
        if (PlayerMoney.instance.currentMoney < GameManager.instance.normalTurretStats.ReturnBuyCost() || waveSpawner.state != WaveSpawner.SpawnState.Counting)
        {
            if (normalTurret.interactable == true)
                normalTurret.interactable = false;
        }
        else if (PlayerMoney.instance.currentMoney >= GameManager.instance.normalTurretStats.ReturnBuyCost())
        {
            if (normalTurret.interactable == false)
                normalTurret.interactable = true;
        }

        // Changing debuff turret interactability according to money and spawn state
        if (PlayerMoney.instance.currentMoney < GameManager.instance.debuffTurretStats.ReturnBuyCost() || waveSpawner.state != WaveSpawner.SpawnState.Counting)
        {
            if (debuffTurret.interactable == true)
                debuffTurret.interactable = false;
        }
        else if (PlayerMoney.instance.currentMoney >= GameManager.instance.debuffTurretStats.ReturnBuyCost())
        {
            if (debuffTurret.interactable == false)
                debuffTurret.interactable = true;
        }

        // Changing heavy turret interactability according to money and spawn state
        if (PlayerMoney.instance.currentMoney < GameManager.instance.heavyTurretStats.ReturnBuyCost() || waveSpawner.state != WaveSpawner.SpawnState.Counting)
        {
            if (heavyTurret.interactable == true)
                heavyTurret.interactable = false;
        }
        else if (PlayerMoney.instance.currentMoney >= GameManager.instance.heavyTurretStats.ReturnBuyCost())
        {
            if (heavyTurret.interactable == false)
                heavyTurret.interactable = true;
        }
    }
}
