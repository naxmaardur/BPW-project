using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : BaseState
{
    PlayerStateMachine _context;
    public PlayerRunState(PlayerStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }

    public override bool CheckSwitchStates()
    {
        if (_context.ControlerContext.IsShouldDodgeSet)
        {
            if (GetSuperState != _context.States.Casting())
            {
                SwitchState(_context.States.Dodge());
                return true;
            }
            else
            {
                _context.ControlerContext.ResetShouldDodge();
            }
        }
        if (_context.ControlerContext.IsShouldSneakSet)
        {
            SwitchState(_context.States.Sneak());
            return true;
        }
        if (!_context.ControlerContext.IsMovementPressed)
        {
            SwitchState(_context.States.Idle());
            return true;
        }
        if (!_context.ControlerContext.IsRunPressed)
        {
            SwitchState(_context.States.Walk());
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
