using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    protected StateFactory _states;
    protected BaseState _currentState;
    public BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    public StateMachine()
    {

    }

    public abstract void OnCheckSwitchStates();
    public abstract void OnFixedUpdate();
    public abstract void OnUpdate();
    public abstract void OnAnimatorMoveState();

}
