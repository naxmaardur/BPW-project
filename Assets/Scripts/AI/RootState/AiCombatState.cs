using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCombatState : BaseState
{
    AiStateMachine _context;
    Vector3 _rootMotion;
    public AiCombatState(AiStateMachine currentContext) : base(currentContext)
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
        if (_context.ControlerContext.Poise == 0)
        {
            SwitchState(_context.States.Stunded());
            return true;
        }
        return false;
    }

    public override void EnterState()
    {
        InitializeSubState();
        _rootMotion = Vector3.zero;
    }

    public override void InitializeSubState()
    {
        if (Vector3.Distance(_context.ControlerContext.PlayerTransfrom.position, _context.ControlerContext.transform.position) > 4)
        {
            SetSubState(_context.States.Follow());
            GetSubState.EnterState();
        }
        else
        {
            SetSubState(_context.States.Fighting());
            GetSubState.EnterState();
        }
    }

    protected override void ExitState()
    {

    }

    protected override void FixedUpdateState()
    {
    }

    protected override void OnAnimatorMoveState()
    {
        Vector3 rootMotion = _rootMotion;
        Vector3 deltaPos = _context.ControlerContext.AnimatorManager.GetDeltaPosition;
        deltaPos.y = 0;
        rootMotion += deltaPos;
        _rootMotion = rootMotion;
    }

    protected override void UpdateState()
    {
        _context.ControlerContext.Move(_rootMotion);
        _rootMotion = Vector3.zero;
    }
}
