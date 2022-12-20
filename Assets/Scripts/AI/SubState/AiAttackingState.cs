using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackingState : BaseState
{
    AiStateMachine _context;
    float _hitboxStartCooldown;
    bool _hitBoxEnabled;
    public AiAttackingState(AiStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }
    public override bool CheckSwitchStates()
    {
        if (AttackIsPlaying()) { return false; }
        SwitchState(_context.States.Combat());
        return true;
    }

    bool AttackIsPlaying()
    {
        if (_context.ControlerContext.AnimatorManager.IsTransitionPlayingWithName("FromAttack")) { return false; }
        return true;
    }

    public override void EnterState()
    {
        _context.ControlerContext.AnimatorManager.TriggerAttack();
        _hitboxStartCooldown = Time.time + 1f;
        _hitBoxEnabled = false;
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    protected override void ExitState()
    {
        _context.ControlerContext.ReturnAttackToken();
        _context.ControlerContext.HitBox.DisableHitBox();
    }

    protected override void FixedUpdateState()
    {
    }

    protected override void OnAnimatorMoveState()
    {
    }

    protected override void UpdateState()
    {
        _context.ControlerContext.HitBox.OnUpdate();
        if (!_hitBoxEnabled && Time.time < _hitboxStartCooldown)
        {
            _context.ControlerContext.HitBox.EnableHitBox();
            _hitBoxEnabled = true;
        }
        if (_hitBoxEnabled && _context.ControlerContext.AnimatorManager.GetAnimationCompletionPecentage() > 75)
        {
            _context.ControlerContext.HitBox.DisableHitBox();
        }

    }
}
