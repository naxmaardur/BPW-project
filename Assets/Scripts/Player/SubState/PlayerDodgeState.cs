using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{

    PlayerStateMachine _context;
    PlayerStateFactory _states;
    float _checkCooldown;
    public PlayerDodgeState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        _context = currentContext;
        _states = playerStateFactory;
    }

    public override bool CheckSwitchStates()
    {
        //throw new System.NotImplementedException();
        if (_context.playerAnimator.IsDodgePlaying()) { return false; }

        if (_context.IsShouldSneakSet)
        {
            SwitchState(_states.Sneak());
            return true;
        }
        if (_context.IsMovementPressed)
        {
            if (_context.IsRunPressed)
                SwitchState(_states.Run());
            else
                SwitchState(_states.Walk());
            return true;
        }

        SwitchState(_states.Idel());
        return true;
    }

    public override void EnterState()
    {
        _context.playerAnimator.TriggerDodge();
        _checkCooldown = 0.7f + Time.time;
    }

    public override void InitializeSubState()
    {
    }

    protected override void ExitState()
    {
        _context.ResetShouldDodge();
    }

    protected override void FixedUpdateState()
    {
    }

    protected override void OnAnimatorMoveState()
    {
    }

    protected override void UpdateState()
    {
        if(_checkCooldown <= Time.time) { CheckSwitchStates(); }
    }
}
