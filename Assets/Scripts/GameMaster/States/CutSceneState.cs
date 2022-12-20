using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class CutSceneState : GameMasterBaseState
{
    GameMasterStateMachine _context;
    public BaseState NextState;


    public CutSceneState(GameMasterStateMachine currentContext) : base(currentContext)
    {
        _IsRootState = true;
        _context = currentContext;
    }
    public override void OnChangeScene()
    {
        _context.ControlerContext.GetNewPlayer();
        _context.ControlerContext.PatrolPathMaster.SetPatrolPaths(Object.FindObjectsOfType<PatrolPath>());
    }
    public void OnCutSceneEnd()
    {
        if (NextState == null) { NextState = _context.States.Game(); }
        SwitchState(NextState);
    }
    public override bool CheckSwitchStates()
    {
        return false;
    }

    public override void EnterState()
    {
        GameMaster.OnCutSceneEnd += OnCutSceneEnd;
    }

    public override void InitializeSubState()
    {
    }

    protected override void ExitState()
    {
        GameMaster.OnCutSceneEnd -= OnCutSceneEnd;
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
