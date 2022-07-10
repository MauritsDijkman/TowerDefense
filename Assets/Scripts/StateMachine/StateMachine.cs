using UnityEngine;

/// <summary>
/// This is the class that is called by other scripts to execute a given State / Behaviour
/// The class is abstract, because we need the other states to be able to implement it and to implement methods that represent the behaviour that the states are going to be responsible for.
/// The state is protected, so that the driving class can deligate behaviour down to the current state.
/// The start function is called, so that the start void is immediatly runned when a new state is set
/// </summary>

public abstract class StateMachine : MonoBehaviour
{
    protected State _state;

    public void SetState(State pState)
    {
        _state = pState;
        StartCoroutine(_state.Start());
    }
}
