using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimatorManager : AnimatorManager
{
    
    RunTimeAnimatorListContainer _animatorContainer;

    public PlayerAnimatorManager(Animator currentAnimator, RunTimeAnimatorListContainer runTimeAnimatorListContainer)
    {
        _animator = currentAnimator;
        _animatorContainer = runTimeAnimatorListContainer;
    }
    public bool ChangeRunTimeAnimatorTo(int animatorID)
    {
        RuntimeAnimatorController runtimeAnimatorController = _animatorContainer.GetAnimator(animatorID);
        if (runtimeAnimatorController == null) { return false; }
        _animator.runtimeAnimatorController = runtimeAnimatorController;
        return true;
    }
    public void SetSneaking(bool value)
    {
        _animator.SetBool("Sneaking", value);
    }
    public void SetRunning(bool value)
    {
        _animator.SetBool("Running", value);
    }
    public void SetAttackComboPos(int value)
    {
        _animator.SetInteger("AttackComboPos", value);
    }
    public int GetAttackComboPos()
    {
        return _animator.GetInteger("AttackComboPos");
    }
    public void TriggerDodge()
    {
        _animator.SetTrigger("Dodge");
    }
    public void TriggerCast()
    {
        _animator.SetTrigger("Cast");
    }
    public void SetAttack(bool value)
    {
        _animator.SetBool("Attack", value);
    }
    public bool GetAttack()
    {
        return _animator.GetBool("Attack");
    }
    public bool TransitioningToAttack()
    {
        return IsTransitionPlayingWithName("ToAttack");
    }
    public bool TransitioningFromAttack()
    {
        return IsTransitionPlayingWithName("FromAttack");
    }

    public bool TransitioningToCast()
    {
        return IsTransitionPlayingWithName("ToCast");
    }
    public bool IsDodgePlaying()
    {
        return IsAnimationPlayingWithName("Dive Forward");
    }

}
