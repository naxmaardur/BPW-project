using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    PlayerStateMachine _context;
    PlayerStateFactory _states;
    float _lastAttackEnd = -200;
    float _maxTimeBetweenCombo = 1f;
    float _checkCooldown;
    Vector3 _rootMotion;
    bool _shouldResetAttack = true;
    public PlayerAttackingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        _IsRootState = true;
        _context = currentContext;
        _states = playerStateFactory;
    }

    public override bool CheckSwitchStates()
    {
        if(_context.IsShouldAttackSet) { return false; }
        if (_context.playerAnimator.IsAnimationPlaying()) { return false; }
        SwitchState(_states.Movement());
        return false;
    }

    public override void EnterState()
    {

        _rootMotion = Vector3.zero;
        _checkCooldown = Time.deltaTime + 2f;
        InitializeSubState();
        NextCombo();
        _context.playerAnimator.SetAttack(true);
    }

    public override void InitializeSubState()
    {

        if (!_context.IsMovementPressed)
        {
            SetSubState(_states.Idel());
            _states.Idel().EnterState();
        }
        else if (_context.IsShouldSneakSet)
        {
            SetSubState(_states.Sneak());
            _states.Sneak().EnterState();
        }
        else if (_context.IsRunPressed)
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
        //_context.ResetShouldAttack();
        _context.playerAnimator.SetAttack(false);
        _lastAttackEnd = Time.time;
    }

    protected override void FixedUpdateState()
    {
    }

    protected override void OnAnimatorMoveState()
    {
        Vector3 rootMotion = _rootMotion;
        Vector3 deltaPos = _context.playerAnimator.GetDeltaPosition;
        deltaPos.y = 0;
        rootMotion += deltaPos;
        _rootMotion = rootMotion;
    }

    protected override void UpdateState()
    {
        if (_checkCooldown <= Time.time)
        {
            if (_context.playerAnimator.GetAnimationCompletionPecentage() > 0.50f && _context.playerAnimator.GetAnimationCompletionPecentage() < 0.80f && _shouldResetAttack)
            {
                _context.ResetShouldAttack();
                _shouldResetAttack = false;
                _context.playerAnimator.SetAttack(false);
                _lastAttackEnd = Time.time;
            }
            
            if(!_shouldResetAttack && _context.playerAnimator.IsAnimationPlaying() && _context.IsShouldAttackSet)
            {
                _context.playerAnimator.SetAttack(true);
            }
            Debug.Log(_shouldResetAttack);
            if (_context.IsShouldAttackSet && !_context.playerAnimator.IsAnimationPlaying())
            {
                Debug.Log("nextcombo");
                NextCombo();
            }
            CheckSwitchStates(); 
        }
        _context.Move(_rootMotion);
        _rootMotion = Vector3.zero;
    }


    private void NextCombo()
    {
        _shouldResetAttack = true;

        if (_lastAttackEnd > Time.time - _maxTimeBetweenCombo)
        {
            int attackPos = _context.playerAnimator.GetAttackComboPos();
            attackPos++;
            if (attackPos > 2) { attackPos = 0; }
            _context.playerAnimator.SetAttackComboPos(attackPos);
        }
        else
        {
            _context.playerAnimator.SetAttackComboPos(0);
        }
    }

}
