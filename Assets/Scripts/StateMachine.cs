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

    public virtual void OnCheckSwitchStates()
    {
        _currentState.CheckSwitchStates();
        _currentState.GetSubState.CheckSwitchStates();
    }


    public virtual void OnFixedUpdate()
    {
        _currentState.FixedUpdateStates();
    }

    public virtual void OnUpdate()
    {
        _currentState.UpdateStates();
    }

    public virtual void OnAnimatorMoveState()
    {
        _currentState.OnAnimatorMoveStates();
    }

}
