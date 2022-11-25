using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
   /* protected PlayerStateMachine _ctx;
    protected PlayerStateFactory _factory;
    private PlayerBaseState _currentSuperState;
    private PlayerBaseState _currentSubState;
    protected bool _IsRootState;
    public PlayerBaseState GetSubState { get { return _currentSubState; } }

    protected PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
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
    protected void SwitchState(PlayerBaseState newState)
    {
        ExitState();

        newState.EnterState();

        if (newState._IsRootState == true)
            _ctx.CurrentState = newState;
        else if (_currentSuperState != null)
            _currentSuperState.SetSubState(newState);
    }
    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }*/
}
