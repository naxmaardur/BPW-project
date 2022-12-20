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
    }
    public override void InitializeSubState()
    {
    }
    protected override void ExitState()
    {
        _context.ControlerContext.playerAnimator.SetRunning(false);
    }
    protected override void FixedUpdateState()
    {
    }
    protected override void OnAnimatorMoveState()
    {
    }
    protected override void UpdateState()
    {
    }
}
