using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : GameMasterBaseState
{
    GameMasterStateMachine _context;
    public DefaultState(GameMasterStateMachine currentContext) : base(currentContext)
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
