using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSneakState : PlayerBaseState
{
    PlayerStateMachine _context;
    PlayerStateFactory _states;
    public PlayerSneakState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
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
        if (!_context.IsShouldSneakSet)
        {
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
            else
            {
                SwitchState(_states.Run());
                return true;
            }
        }
        return false;
    }

    public override void EnterState()
    {
        _context.playerAnimator.SetSneaking(true);
    }

    public override void InitializeSubState()
    {
    }

    protected override void ExitState()
    {
        _context.playerAnimator.SetSneaking(false);
    }

    protected override void FixedUpdateState()
    {
       // throw new System.NotImplementedException();
    }

    protected override void OnAnimatorMoveState()
    {
       // throw new System.NotImplementedException();
    }

    protected override void UpdateState()
    {
        CheckSwitchStates();
    }
}
