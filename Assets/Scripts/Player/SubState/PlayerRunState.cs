using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : BaseState
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
        if (_context.ControlerContext.IsShouldDodgeSet)
        {
            if (GetSuperState != _states.Casting())
            {
                SwitchState(_states.Dodge());
                return true;
            }
            else
            {
                _context.ControlerContext.ResetShouldDodge();
            }
        }
        if (_context.ControlerContext.IsShouldSneakSet)
        {
            SwitchState(_states.Sneak());
            return true;
        }
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
        return false;
    }

    public override void EnterState()
    {
        _context.ControlerContext.playerAnimator.SetRunning(true);
        //throw new System.NotImplementedException();
    }

    public override void InitializeSubState()
    {
        //throw new System.NotImplementedException();
    }

    protected override void ExitState()
    {
        _context.ControlerContext.playerAnimator.SetRunning(false);
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
    }
}
