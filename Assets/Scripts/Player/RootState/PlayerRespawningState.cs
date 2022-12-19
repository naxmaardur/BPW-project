using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawningState : BaseState
{
    PlayerStateMachine _context;

    public PlayerRespawningState(PlayerStateMachine currentContext) : base(currentContext)
    {
        _IsRootState = true;
        _context = currentContext;
    }

    public override bool CheckSwitchStates()
    {
        if (_context.ControlerContext.playerAnimator.TransitioningFromRespawn())
        {
            SwitchState(_context.States.Movement());
        }
        return false;
    }

    public override void EnterState()
    {
        GameMaster.Instance.RestartGameScene();
        _context.ControlerContext.playerAnimator.TriggerRespawn();
        _context.ControlerContext.Health = 10000;
        _context.ControlerContext.transform.position = _context.ControlerContext.PlayerSpawnPoint.position;
        _context.ControlerContext.transform.rotation = _context.ControlerContext.PlayerSpawnPoint.rotation;
        _context.ControlerContext.PlaySource();
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
