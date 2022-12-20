using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : GameMasterBaseState
{
    GameMasterStateMachine _context;
    public MenuState(GameMasterStateMachine currentContext) : base(currentContext)
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
        _context.ControlerContext.Player?.DisableInput();
        Time.timeScale = 0.0f;
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
