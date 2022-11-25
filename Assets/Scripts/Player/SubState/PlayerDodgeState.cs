using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : BaseState
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
        if (_checkCooldown > Time.time){ return false; }

            //throw new System.NotImplementedException();
            if (_context.ControlerContext.playerAnimator.IsDodgePlaying()) { return false; }

        if (_context.ControlerContext.IsShouldSneakSet)
        {
            SwitchState(_states.Sneak());
            return true;
        }
        if (_context.ControlerContext.IsMovementPressed)
        {
            if (_context.ControlerContext.IsRunPressed)
                SwitchState(_states.Run());
            else
                SwitchState(_states.Walk());
            return true;
        }

        SwitchState(_states.Idle());
        return true;
    }

    public override void EnterState()
    {
        _context.ControlerContext.invincible = true;
        _context.ControlerContext.playerAnimator.TriggerDodge();
        _checkCooldown = 0.7f + Time.time;
    }

    public override void InitializeSubState()
    {
    }

    protected override void ExitState()
    {
        _context.ControlerContext.ResetShouldDodge();
    }

    protected override void FixedUpdateState()
    {
    }

    protected override void OnAnimatorMoveState()
    {
    }

    protected override void UpdateState()
    {
        if (_context.ControlerContext.playerAnimator.GetAnimationCompletionPecentage() > 0.70f && _context.ControlerContext.invincible)
        {
            _context.ControlerContext.invincible = false;
        }
    }
}
