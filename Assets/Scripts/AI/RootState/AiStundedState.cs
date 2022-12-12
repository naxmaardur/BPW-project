using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStundedState : BaseState
{
    AiStateMachine _context;
    public AiStundedState(AiStateMachine currentContext) : base(currentContext)
    {
        _IsRootState = true;
        _context = currentContext;
    }
    public override bool CheckSwitchStates()
    {
        if (_context.ControlerContext.Health == 0)
        {
            SwitchState(_context.States.Dying());
            return true;
        }
        if (_context.ControlerContext.AnimatorManager.IsStunPlaying()) { return false; }
        SwitchState(_context.States.Combat());
        return false;
    }

    public override void EnterState()
    {
        _context.ControlerContext.AnimatorManager.TriggerStun();
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
