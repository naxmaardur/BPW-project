using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : BaseState
{
    PlayerStateMachine _context;
    float _lastAttackEnd = -200;
    float _maxTimeBetweenCombo = 1f;
    Vector3 _rootMotion;
    bool _shouldResetAttack = true;
    public PlayerAttackingState(PlayerStateMachine currentContext) : base(currentContext)
    {
        _IsRootState = true;
        _context = currentContext;
    }
    public override bool CheckSwitchStates()
    {

        if (_context.ControlerContext.Health <= 0)
        {
            SwitchState(_context.States.Dying());
            return true;
        }
        if (GetSubState == _context.States.Dodge())
        {
            SwitchState(_context.States.Movement());
            return true;
        }
        if (_context.ControlerContext.playerAnimator.TransitioningToAttack()) { return false; }

        if (_context.ControlerContext.IsShouldAttackSet) { return false; }
        if (_context.ControlerContext.playerAnimator.IsAnimationPlaying()) { return false; }
        if (_context.ControlerContext.IsShouldCastSet)
        {
            SwitchState(_context.States.Casting());
            return true;
        }
        SwitchState(_context.States.Movement());

        return true;
    }
    public override void EnterState()
    {
        _context.ControlerContext.CurrentWeaponHitBox.EnableHitBox();
        _rootMotion = Vector3.zero;
        InitializeSubState();
        NextCombo();
        _context.ControlerContext.playerAnimator.SetAttack(true);
    }
    public override void InitializeSubState()
    {

        if (!_context.ControlerContext.IsMovementPressed)
        {
            SetSubState(_context.States.Idle());
            _context.States.Idle().EnterState();
        }
        else if (_context.ControlerContext.IsShouldSneakSet)
        {
            SetSubState(_context.States.Sneak());
            _context.States.Sneak().EnterState();
        }
        else if (_context.ControlerContext.IsRunPressed)
        {
            SetSubState(_context.States.Run());
            _context.States.Run().EnterState();
        }
        else
        {
            SetSubState(_context.States.Walk());
            _context.States.Walk().EnterState();
        }

    }
    protected override void ExitState()
    {
        _context.ControlerContext.CurrentWeaponHitBox.DisableHitBox();
        _context.ControlerContext.playerAnimator.SetAttack(false);
        _lastAttackEnd = Time.time;
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
        _context.ControlerContext.CurrentWeaponHitBox.OnUpdate();
        if (_context.ControlerContext.playerAnimator.GetAnimationCompletionPecentage() > 0.50f && _context.ControlerContext.playerAnimator.GetAnimationCompletionPecentage() < 0.80f && _shouldResetAttack)
        {
            _context.ControlerContext.ResetShouldAttack();
            _shouldResetAttack = false;
            _context.ControlerContext.playerAnimator.SetAttack(false);
            _lastAttackEnd = Time.time;
        }

        if (!_shouldResetAttack && _context.ControlerContext.playerAnimator.IsAnimationPlaying() && _context.ControlerContext.IsShouldAttackSet)
        {
            _context.ControlerContext.playerAnimator.SetAttack(true);
        }
        if (_context.ControlerContext.IsShouldAttackSet && !_context.ControlerContext.playerAnimator.IsAnimationPlaying() && _context.ControlerContext.playerAnimator.GetAttack() && !_shouldResetAttack)
        {
            NextCombo();
        }
        _context.ControlerContext.Move(_rootMotion);
        _rootMotion = Vector3.zero;
    }
    private void NextCombo()
    {
        _shouldResetAttack = true;
        _context.ControlerContext.CurrentWeaponHitBox.ResetHitList();
        if (_lastAttackEnd > Time.time - _maxTimeBetweenCombo)
        {
            int attackPos = _context.ControlerContext.playerAnimator.GetAttackComboPos();
            attackPos++;
            if (attackPos > 2) { attackPos = 0; }
            _context.ControlerContext.playerAnimator.SetAttackComboPos(attackPos);
        }
        else
        {
            _context.ControlerContext.playerAnimator.SetAttackComboPos(0);
        }
    }
}
