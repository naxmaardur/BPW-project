using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : BaseState
{
    AiStateMachine _context;
    AiStateFactory _states;
    float _IdleEndTime;
    public AiIdleState(AiStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }
    public override bool CheckSwitchStates()
    {
        if (Time.time < _IdleEndTime) { return false; }
        if (_context.ControlerContext.patrol)
        {
            SwitchState(_context.States.Patrol());
            return true;
        }
        SwitchState(_context.States.Wander());
        return true;
    }
    public override void EnterState()
    {
        _IdleEndTime = Time.time + _context.ControlerContext.IdleTime;
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
