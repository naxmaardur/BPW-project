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
       if(_context.ControlerContext.AnimatorManager.IsAnimationPlayingWithName("Spawning")) { return false; }
       SwitchState(_context.States.Default());
       return true;
    }

    public override void EnterState()
    {
        ///
    }

    public override void InitializeSubState()
    {
     //has no SubStates.
    }

    protected override void ExitState()
    {
       // throw new System.NotImplementedException();
    }

    protected override void FixedUpdateState()
    {
     //   throw new System.NotImplementedException();
    }

    protected override void OnAnimatorMoveState()
    {
     //   throw new System.NotImplementedException();
    }

    protected override void UpdateState()
    {
     //   throw new System.NotImplementedException();
    }
}
