using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : BaseState
{

    PlayerStateMachine _context;
    float _checkCooldown;
    public PlayerDodgeState(PlayerStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }

    public override bool CheckSwitchStates()
    {
        if (_checkCooldown > Time.time){ return false; }

            //throw new System.NotImplementedException();
            if (_context.ControlerContext.playerAnimator.IsDodgePlaying()) { return false; }

        if (_context.ControlerContext.IsShouldSneakSet)
        {
            SwitchState(_context.States.Sneak());
            return true;
        }
        if (_context.ControlerContext.IsMovementPressed)
        {
            if (_context.ControlerContext.IsRunPressed)
                SwitchState(_context.States.Run());
            else
                SwitchState(_context.States.Walk());
            return true;
        }

        SwitchState(_context.States.Idle());
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
