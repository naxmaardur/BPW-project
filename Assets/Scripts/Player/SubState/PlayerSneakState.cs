using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSneakState : BaseState
{
    PlayerStateMachine _context;
    public PlayerSneakState(PlayerStateMachine currentContext) : base(currentContext)
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
        if (!_context.ControlerContext.IsShouldSneakSet)
        {
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
            else
            {
                SwitchState(_context.States.Run());
                return true;
            }
        }
        return false;
    }

    public override void EnterState()
    {
        _context.ControlerContext.playerAnimator.SetSneaking(true);
        _context.ControlerContext.SetHeight(1f, new Vector3(0,-0.5f,0));
    }

    public override void InitializeSubState()
    {
    }

    protected override void ExitState()
    {
        _context.ControlerContext.playerAnimator.SetSneaking(false);
        _context.ControlerContext.SetHeight(2f,Vector3.zero);
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
    }
}
