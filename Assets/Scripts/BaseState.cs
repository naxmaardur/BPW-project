using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected StateMachine _ctx;
    protected StateFactory _factory;
    private BaseState _currentSuperState;
    private BaseState _currentSubState;
    protected bool _IsRootState;
    public BaseState GetSubState { get { return _currentSubState; } }
    public BaseState GetSuperState { get { return _currentSuperState; } }

    protected BaseState(StateMachine currentContext, StateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    public abstract void EnterState();
    protected abstract void ExitState();
    public abstract bool CheckSwitchStates();
    public abstract void InitializeSubState();

    protected abstract void UpdateState();
    protected abstract void FixedUpdateState();
    protected abstract void OnAnimatorMoveState();

    //Calling Updates on Both the Super and the sub states
    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
            _currentSubState.UpdateStates();
    }
    public void FixedUpdateStates()
    {
        FixedUpdateState();
        if (_currentSubState != null)
            _currentSubState.FixedUpdateStates();
    }

    public void OnAnimatorMoveStates()
    {
        OnAnimatorMoveState();
        if (_currentSubState != null)
            _currentSubState.OnAnimatorMoveStates();
    }

    //State Changing
    protected void SwitchState(BaseState newState)
    {
        ExitState();

        newState.EnterState();

        if (newState._IsRootState == true)
            _ctx.CurrentState = newState;
        else if (_currentSuperState != null)
            _currentSuperState.SetSubState(newState);
    }
    protected void SetSuperState(BaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(BaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
