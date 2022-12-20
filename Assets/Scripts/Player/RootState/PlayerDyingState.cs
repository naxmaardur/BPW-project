using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDyingState : BaseState
{
    PlayerStateMachine _context;
    public PlayerDyingState(PlayerStateMachine currentContext) : base(currentContext)
    {
        _IsRootState = true;
        _context = currentContext;
    }

    public override bool CheckSwitchStates()
    {
        if (_context.ControlerContext.playerAnimator.TransitioningToDead())
        {
            SwitchState(_context.States.Respawn());
            return true;
        }
        return false;
    }

    public override void EnterState()
    {
        _context.ControlerContext.playerAnimator.TriggerDie();
    }

    public override void InitializeSubState()
    {
    }

    protected override void ExitState()
    {
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
