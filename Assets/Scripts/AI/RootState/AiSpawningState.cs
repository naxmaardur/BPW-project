using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawningState : BaseState
{
    AiStateMachine _context;
    public AiSpawningState(AiStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }
    public override bool CheckSwitchStates()
    {
        throw new System.NotImplementedException();
    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    protected override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    protected override void FixedUpdateState()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnAnimatorMoveState()
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}