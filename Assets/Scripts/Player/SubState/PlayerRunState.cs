using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    PlayerStateMachine _context;
    PlayerStateFactory _states;
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        _context = currentContext;
        _states = playerStateFactory;
    }

    public override bool CheckSwitchStates()
    {
        if (_context.IsShouldDodgeSet)
        {
            SwitchState(_states.Dodge());
            return true;
        }
        if (_context.IsShouldSneakSet)
        {
            SwitchState(_states.Sneak());
            return true;
        }
        if (!_context.IsMovementPressed)
        {
            SwitchState(_states.Idel());
            return true;
        }
        if (!_context.IsRunPressed)
        {
            SwitchState(_states.Walk());
            return true;
        }
        return false;
    }

    public override void EnterState()
    {
        _context.playerAnimator.SetRunning(true);
        //throw new System.NotImplementedException();
    }

    public override void InitializeSubState()
    {
        //throw new System.NotImplementedException();
    }

    protected override void ExitState()
    {
        _context.playerAnimator.SetRunning(false);
        // throw new System.NotImplementedException();
    }

    protected override void FixedUpdateState()
    {
        //throw new System.NotImplementedException();
    }

    protected override void OnAnimatorMoveState()
    {
        //throw new System.NotImplementedException();
    }

    protected override void UpdateState()
    {
        CheckSwitchStates();
    }
}
