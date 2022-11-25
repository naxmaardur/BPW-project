using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSneakState : BaseState
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
        if (_context.ControlerContext.IsShouldDodgeSet)
        {
            SwitchState(_states.Dodge());
            return true;
        }
        if (!_context.ControlerContext.IsShouldSneakSet)
        {
            if (!_context.ControlerContext.IsMovementPressed)
            {
                SwitchState(_states.Idle());
                return true;
            }
            if (!_context.ControlerContext.IsRunPressed)
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
        _context.ControlerContext.playerAnimator.SetSneaking(true);
    }

    public override void InitializeSubState()
    {
    }

    protected override void ExitState()
    {
        _context.ControlerContext.playerAnimator.SetSneaking(false);
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
