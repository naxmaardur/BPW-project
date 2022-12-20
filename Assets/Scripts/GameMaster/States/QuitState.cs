using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class QuitState : GameMasterBaseState
{
    GameMasterStateMachine _context;


    public QuitState(GameMasterStateMachine currentContext) : base(currentContext)
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
        Application.Quit();
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
