using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDyingState : BaseState
{
    AiStateMachine _context;
    public AiDyingState(AiStateMachine currentContext) : base(currentContext)
    {
        _IsRootState = true;
        _context = currentContext;
    }
    public override bool CheckSwitchStates()
    {
        return false;
    }
    public override void EnterState()
    {
        _context.ControlerContext.AnimatorManager.TriggerDeath();
        _context.ControlerContext.DisableCollision();
        _context.ControlerContext.SpawnDeathDropObject();
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
        if (_context.ControlerContext.AnimatorManager.IsAnimationPlayingWithName("Death")
            || _context.ControlerContext.AnimatorManager.IsTransitionPlayingWithName("ToDeath")) { return; }
        _context.ControlerContext.DestroySelf();
    }
}
