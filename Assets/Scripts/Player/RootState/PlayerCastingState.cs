using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastingState : BaseState
{
    PlayerStateMachine _context;
    PlayerStateFactory _states;
    Vector3 _rootMotion;
    bool _hascast;
    public PlayerCastingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        _IsRootState = true;
        _context = currentContext;
        _states = playerStateFactory;
    }

    public override bool CheckSwitchStates()
    {
        if (_context.ControlerContext.Health <= 0)
        {
            SwitchState(_states.Dying());
            return true;
        }
        if (!_hascast) { return false; }
        if (_context.ControlerContext.playerAnimator.TransitioningToCast()) { return false; }
        if (_context.ControlerContext.playerAnimator.TransitioningFromAttack()) { return false; }// needed for some reason
        if (_context.ControlerContext.playerAnimator.IsAnimationPlayingWithName("Cast")) { return false; }
        if (_context.ControlerContext.IsShouldAttackSet) { SwitchState(_states.Attacking()); return true; }
        SwitchState(_states.Movement());
        return true;
    }

    public override void EnterState()
    {
        _hascast = false;
        _context.ControlerContext.MagicContainer.EnableContainer();
        _rootMotion = Vector3.zero;
        InitializeSubState();
        _context.ControlerContext.playerAnimator.TriggerCast();
    }

    public override void InitializeSubState()
    {

        if (!_context.ControlerContext.IsMovementPressed)
        {
            SetSubState(_states.Idle());
            _states.Idle().EnterState();
        }
        else if (_context.ControlerContext.IsShouldSneakSet)
        {
            SetSubState(_states.Sneak());
            _states.Sneak().EnterState();
        }
        else if (_context.ControlerContext.IsRunPressed)
        {
            SetSubState(_states.Run());
            _states.Run().EnterState();
        }
        else
        {
            SetSubState(_states.Walk());
            _states.Walk().EnterState();
        }

    }

    protected override void ExitState()
    {
        _context.ControlerContext.MagicContainer.DisableContainer();
        _context.ControlerContext.ResetShouldCast();
        //_context.ResetShouldAttack();
        //_context.ControlerContext.playerAnimator.SetAttack(false);
    }

    protected override void FixedUpdateState()
    {
    }

    protected override void OnAnimatorMoveState()
    {
        Vector3 rootMotion = _rootMotion;
        Vector3 deltaPos = _context.ControlerContext.playerAnimator.GetDeltaPosition;
        deltaPos.y = 0;
        rootMotion += deltaPos;
        _rootMotion = rootMotion;
    }

    protected override void UpdateState()
    {
        _context.ControlerContext.Move(_rootMotion);
        _rootMotion = Vector3.zero;
        if (_context.ControlerContext.playerAnimator.TransitioningToCast()) { return; }
        if (!_context.ControlerContext.playerAnimator.IsAnimationPlayingWithName("Cast")) { return; }
        if (!_hascast && _context.ControlerContext.playerAnimator.GetAnimationCompletionPecentage() >= 0.4f)
        {
            _hascast = true;
            _context.ControlerContext.MagicContainer.OnCast();
            PlayerControler.OnSpellUpdate(_context.ControlerContext.MagicContainer);
        }

    }

}
