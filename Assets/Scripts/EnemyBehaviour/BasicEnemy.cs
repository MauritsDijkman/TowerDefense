using UnityEngine;

[RequireComponent(typeof(MovementBehaviour))]
[RequireComponent(typeof(MoneyDropBehaviour))]
[RequireComponent(typeof(HealthBehaviour))]

public class BasicEnemy : MonoBehaviour
{
    private int _level;
    private float _health;
    private float _speed;
    private float _worth;
    private float _damage;

    private CommandStack _commandStack = new CommandStack();

    private MovementBehaviour _movementBehaviour = null;
    private MoneyDropBehaviour _moneyDropBehaviour = null;
    private HealthBehaviour _healthBehaviour = null;

    private void Awake()
    {
        _movementBehaviour = GetComponent<MovementBehaviour>();
        _moneyDropBehaviour = GetComponent<MoneyDropBehaviour>();
        _healthBehaviour = GetComponent<HealthBehaviour>();
    }

    private void Start()
    {
        if (_healthBehaviour != null)
            _healthBehaviour.SetStartHealth(GetHealth());
    }

    private void Update()
    {
        if (GameManager.instance != null)
            if (!GameManager.instance.inputEnabled)
                return;

        MoneyDropHandler();
        DamageToCastleHandler();
        MoveHandler();
    }

    private void MoveHandler()
    {
        if (_movementBehaviour != null)
        {
            _movementBehaviour.SetSpeed(GetSpeed());
            _movementBehaviour.ExecuteBehaviour();
        }
    }

    private void MoneyDropHandler()
    {
        if (_healthBehaviour != null)
        {
            if (_healthBehaviour.ReturnHealth() <= 0)
            {
                if (_moneyDropBehaviour != null)
                {
                    _moneyDropBehaviour.SetWealth(GetWorth());
                    _moneyDropBehaviour.ExecuteBehaviour();
                }
                Destroy(gameObject);
                return;
            }
        }
    }

    private void DamageToCastleHandler()
    {
        //Debug.Log($"WaypointIndex: {_movementBehaviour.ReturnWaypointIndex()} || WaypointsLength: {Waypoints.waypoints.Length}");

        if (_movementBehaviour != null && Waypoints.waypoints != null)
        {
            if (_movementBehaviour.ReturnWaypointIndex() >= Waypoints.waypoints.Length)
            {
                _commandStack.ExecuteCommand(new DamageBehaviour(GetDamage()));
                Destroy(gameObject);
                return;
            }
        }
    }

    public void DoDamageToEnemy(float pDamage)
    {
        _healthBehaviour.ExecuteBehaviour(pDamage);
    }


    // ---------------------------- Set values ---------------------------- //

    public void SetLevel(int pLevel)
    {
        _level = pLevel;
    }

    public void SetHealth(float pHealth)
    {
        _health = pHealth;
    }

    public void SetSpeed(float pSpeed)
    {
        _speed = pSpeed;
    }

    public void SetWorth(float pWorth)
    {
        _worth = pWorth;
    }

    public void SetDamage(float pDamage)
    {
        _damage = pDamage;
    }


    // ---------------------------- Get values ---------------------------- //

    public int GetLevel()
    {
        return _level;
    }

    public float GetHealth()
    {
        return _health;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public float GetWorth()
    {
        return _worth;
    }

    public float GetDamage()
    {
        return _damage;
    }
}
