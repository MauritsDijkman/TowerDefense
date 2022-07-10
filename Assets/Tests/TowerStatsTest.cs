using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TowerStatsTest
{
    private TurretSpawner turretSpawner;
    private CanvasTurret canvasTurret;

    string turretName = "NormalTurret";
    int turretLevel = 1;
    float turretDamage = 1;
    float turretRange = 3.5f;
    float turretFirRate = 1.5f;
    int turretBuyCost = 150;
    int turretUpgradeCost = 250;
    int turretSellCost = 75;

    //OneTimeSetup is a NUnit attribute, it identify methods that are called once
    //prior to executing any of the tests.
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TurretTestScene");
    }

    //UnitySetup is a Unity Test Framework attribute that allows yielding instructions,
    //it can be used to set up tests in the test Scene, e.g. call  yield return new WaitForSeconds
    //to ensure all the GameObjects in the Scene are loaded.

    [UnitySetUp]
    public IEnumerator SetupTests()
    {
        //Wait for the scene to finish loading. 
        yield return new WaitForSeconds(0.5f);

        //Then setup the tests.
        turretSpawner = GameObject.FindObjectOfType<TurretSpawner>();
        canvasTurret = GameObject.FindObjectOfType<CanvasTurret>();
    }

    [UnityTest]
    public IEnumerator Spawner_Spawns_Correct_Tower_Test()
    {
        yield return new WaitForEndOfFrame();

        Turret spawnedTurret = turretSpawner.GetNormalTurret();

        yield return new WaitForSeconds(1f);

        Assert.AreEqual(turretName, spawnedTurret.TurretType);
        Assert.AreEqual(turretLevel, spawnedTurret.Level);
        Assert.AreEqual(turretDamage, spawnedTurret.Damage);
        Assert.AreEqual(turretRange, spawnedTurret.Range);
        Assert.AreEqual(turretFirRate, spawnedTurret.FireRate);
        Assert.AreEqual(turretBuyCost, spawnedTurret.BuyCost);
        Assert.AreEqual(turretUpgradeCost, spawnedTurret.UpgradeCost);
        Assert.AreEqual(turretSellCost, spawnedTurret.SellCost);
    }

    [UnityTest]
    public IEnumerator Canvas_Turret_Spawns_Correct_Tower_Test()
    {
        yield return new WaitForEndOfFrame();

        canvasTurret.SpawnNormalTurret();
        //canvasTurret.SpawnHeavyTurret();
        //canvasTurret.SpawnDebuffTurret();

        yield return new WaitForSeconds(1f);

        Turret turretToBuild = canvasTurret.turretToBuild;

        Assert.AreEqual(turretName, turretToBuild.TurretType);
        Assert.AreEqual(turretLevel, turretToBuild.Level);
        Assert.AreEqual(turretDamage, turretToBuild.Damage);
        Assert.AreEqual(turretRange, turretToBuild.Range);
        Assert.AreEqual(turretFirRate, turretToBuild.FireRate);
        Assert.AreEqual(turretBuyCost, turretToBuild.BuyCost);
        Assert.AreEqual(turretUpgradeCost, turretToBuild.UpgradeCost);
        Assert.AreEqual(turretSellCost, turretToBuild.SellCost);
    }

    [UnityTest]
    public IEnumerator State_Machine_Value_Change_Test()
    {
        yield return new WaitForEndOfFrame();

        canvasTurret.SpawnNormalTurret();

        yield return new WaitForSeconds(1f);

        Shooting_StateMachine spawnedTurret = GameObject.Find("NormalTurret(Clone)").GetComponent<Shooting_StateMachine>();

        spawnedTurret._range = 0;

        Assert.GreaterOrEqual(turretRange, spawnedTurret._range);
    }
}
