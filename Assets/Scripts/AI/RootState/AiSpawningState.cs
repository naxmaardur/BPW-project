using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawningState : BaseState
{
    AiStateMachine _context;
    public AiSpawningState(AiStateMachine currentContext) : base(currentContext)
    {
        _IsRootState = true;
        _context = currentContext;
    }
    public override bool CheckSwitchStates()
    {
        if (_context.ControlerContext.AnimatorManager.IsAnimationPlayingWithName("Spawning")) { return false; }
        SwitchState(_context.States.Default());
        return true;
    }
    public override void EnterState()
    {
    }
    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
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
