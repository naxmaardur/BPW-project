using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : GameMasterBaseState
{

    GameMasterStateMachine _context;
    public GameState(GameMasterStateMachine currentContext) : base(currentContext)
    {
        _IsRootState = true;
        _context = currentContext;
    }

    public override void OnChangeScene()
    {
        _context.ControlerContext.GetNewPlayer();
        _context.ControlerContext.PatrolPathMaster.SetPatrolPaths(Object.FindObjectsOfType<PatrolPath>());
    }

    public override bool CheckSwitchStates()
    {
        return false;
    }

    public override void EnterState()
    {
        _context.ControlerContext.Player?.onAwake();
        _context.ControlerContext.Player?.EnableInput();
        Time.timeScale = 1.0f;
    }

    public override void InitializeSubState()
    {
    }

    protected override void ExitState()
    {
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
        _context.ControlerContext.Player?.OnUpdate();
        _context.ControlerContext.InvokeUpdateGlobalHitboxes();
        _context.ControlerContext.InvokeUpdateAI();
    }
}
