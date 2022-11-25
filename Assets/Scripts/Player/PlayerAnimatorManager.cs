using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimatorManager
{
    Animator _animator;
    RunTimeAnimatorListContainer _animatorContainer;


    float _targetX;
    float _targetY;


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



    public IEnumerator Updatefloats()
    {
        while (true)
        {
            yield return new WaitWhile(() =>
            {

                return _animator.GetFloat("MovementX") == _targetX && _animator.GetFloat("MovementY") == _targetY;
            });

            _animator.SetFloat("MovementX", _targetX, 0.1f, Time.deltaTime);
            _animator.SetFloat("MovementY", _targetY, 0.1f, Time.deltaTime);
        }
    }

    public Vector3 GetDeltaPosition { get { return _animator.deltaPosition; } }

    public void SetMovementX(float value)
    {
        _animator.SetFloat("MovementX", value);
        _targetX = value;
    }
    public void SetMovementY(float value)
    {
        _animator.SetFloat("MovementY", value);
        _targetY = value;
    }
    public void SetMovementXWithDamp(float value)
    {
        _targetX = value;
    }
    public void SetMovementYWithDamp(float value)
    {
        _targetY = value;
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
    public bool IsAnimationPlaying()
    {
        return (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f && !_animator.IsInTransition(0);
    }

    public float GetAnimationCompletionPecentage()
    {
        return (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1;
    }


    public bool TransitioningToAttack()
    {
        AnimatorTransitionInfo transitionInfo = _animator.GetAnimatorTransitionInfo(0);
        return transitionInfo.IsUserName("ToAttack");
    }
    public bool TransitioningFromAttack()
    {
        AnimatorTransitionInfo transitionInfo = _animator.GetAnimatorTransitionInfo(0);
        return transitionInfo.IsUserName("FromAttack");
    }

    public bool TransitioningToCast()
    {
        AnimatorTransitionInfo transitionInfo = _animator.GetAnimatorTransitionInfo(0);
        return transitionInfo.IsUserName("ToCast");
    }
    public bool IsDodgePlaying()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("Dive Forward");
    }
}
