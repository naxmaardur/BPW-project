using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDyingState : BaseState
{
    PlayerStateMachine _context;
    PlayerStateFactory _states;

    public PlayerDyingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        _IsRootState = true;
        _context = currentContext;
        _states = playerStateFactory;
    }

    public override bool CheckSwitchStates()
    {
        throw new System.NotImplementedException();
    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    protected override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    protected override void FixedUpdateState()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnAnimatorMoveState()
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
