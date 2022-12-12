using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAnimatorManager : AnimatorManager
{
    public AiAnimatorManager(Animator animator) : base(animator)
    {
    }


    public void SetWalkforward(bool b)
    {
        _animator.SetBool("WalkForward", b);
    }
    public void SetRunforward(bool b)
    {
        _animator.SetBool("RunForward", b);
    }

    public void SetCombatMovement(bool b)
    {
        _animator.SetBool("Combat", b);
    }

    public void TriggerDeath()
    {
        _animator.SetTrigger("Die");
    }

    public void TriggerImpact()
    {
        _animator.SetTrigger("Impact");
    }
    public void TriggerStun()
    {
        _animator.SetTrigger("Stuned");
    }
    public void TriggerAttack()
    {
        _animator.SetInteger("AttackNumber", Random.Range(0, _animator.GetInteger("AttackTotal")));
        _animator.SetTrigger("Attack");
    }


    public bool IsStunPlaying()
    {
        return IsAnimationPlayingWithName("Stun") || IsAnimationPlayingWithName("Stun 0") || IsTransitionPlayingWithName("ToStun");
    }
}
