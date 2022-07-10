using System.Collections;

/// <summary>
/// This is the base class of the whole State Machine
/// The class is abstract, because we need the other states to be able to implement it and to implement methods that represent the behaviour that the states are going to be responsible for.
/// The IEnumerator is virtual, so that it can be overwritten by the deriving class.
/// The Shooting_StateMachine is protected, so that it can be referenced in the deriving classes.
/// </summary>

public abstract class State
{
    protected Shooting_StateMachine _shooting_StateMachine;

    public State(Shooting_StateMachine pShooting_StateMachine)
    {
        _shooting_StateMachine = pShooting_StateMachine;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator Search()
    {
        yield break;
    }

    public virtual IEnumerator Attack()
    {
        yield break;
    }
}
