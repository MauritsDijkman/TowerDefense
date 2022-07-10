using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class EnemyTest
{
    private EnemySpawner enemySpawner;

    string normalEnemyName = "NormalEnemy(Clone)";
    string fastEnemyName = "FastEnemy(Clone)";
    string tankEnemyName = "TankEnemy(Clone)";
    string bossEnemyName = "BossEnemy(Clone)";

    private float minimumSpeed = 1f;
    private NormalEnemyFactory normalEnemyFactory;
    private FastEnemyFactory fastEnemyFactory;
    private TankEnemyFactory tankEnemyFactory;
    private BossEnemyFactory bossEnemyFactory;

    //OneTimeSetup is a NUnit attribute, it identify methods that are called once
    //prior to executing any of the tests.
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("EnemyTestScene");
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
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();

        //Then setup the tests.
        normalEnemyFactory = GameObject.FindObjectOfType<NormalEnemyFactory>();
        fastEnemyFactory = GameObject.FindObjectOfType<FastEnemyFactory>();
        tankEnemyFactory = GameObject.FindObjectOfType<TankEnemyFactory>();
        bossEnemyFactory = GameObject.FindObjectOfType<BossEnemyFactory>();
    }

    [UnityTest]
    public IEnumerator Spawner_Spawns_Enemy_Type_Test()
    {
        //yield return null wait for the next frame, this is to ensure testSpawner is properly instantiated.
        yield return new WaitForEndOfFrame();

        //enemySpawner.SpawnNormalEnemy();
        //enemySpawner.SpawnFastEnemy();
        //enemySpawner.SpawnTankEnemy();
        enemySpawner.SpawnBossEnemy();

        yield return new WaitForSeconds(2f);

        string foundEnemyName = GameObject.FindObjectOfType<BasicEnemy>().gameObject.name;

        //Assert.AreEqual(normalEnemyName, foundEnemyName);
        //Assert.AreEqual(fastEnemyName, foundEnemyName);
        //Assert.AreEqual(tankEnemyName, foundEnemyName);
        Assert.AreEqual(bossEnemyName, foundEnemyName);
    }

    [UnityTest]
    public IEnumerator Enemy_Cannot_Get_Negative_Damage()
    {
        enemySpawner.SpawnTankEnemy();

        yield return new WaitForEndOfFrame();

        HealthBehaviour foundEnemy = GameObject.FindObjectOfType<HealthBehaviour>();

        yield return new WaitForEndOfFrame();

        Assert.Throws<System.ArgumentOutOfRangeException>(delegate
        {
            foundEnemy.ExecuteBehaviour(-5f);
        });
    }

    [UnityTest]
    public IEnumerator Factory_Enemy_Minimum_Speed_Test()
    {
        yield return new WaitForEndOfFrame();
        Assert.LessOrEqual(minimumSpeed, normalEnemyFactory.GetEnemy().Speed);
        Assert.LessOrEqual(minimumSpeed, fastEnemyFactory.GetEnemy().Speed);
        Assert.LessOrEqual(minimumSpeed, tankEnemyFactory.GetEnemy().Speed);
        Assert.LessOrEqual(minimumSpeed, bossEnemyFactory.GetEnemy().Speed);
    }


    [UnityTest]
    public IEnumerator Enemy_Cannot_Get_Negative_Debuff_Time()
    {
        enemySpawner.SpawnNormalEnemy();
        yield return new WaitForEndOfFrame();

        DebuffBehaviour foundEnemy = GameObject.FindObjectOfType<DebuffBehaviour>();

        Assert.Throws<System.ArgumentOutOfRangeException>(delegate
        {
            foundEnemy.ExecuteDebuff(-3f);
        });
    }

    [UnityTest]
    public IEnumerator Enemy_Cannot_Do_Negative_Damage_To_Castle_HP()
    {
        enemySpawner.SpawnNormalEnemy();
        yield return new WaitForEndOfFrame();

        CommandStack _commandStack = new CommandStack();

        Assert.Throws<System.ArgumentOutOfRangeException>(delegate
        {
            _commandStack.ExecuteCommand(new DamageBehaviour(-5));
        });
    }
}
