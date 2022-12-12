using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAnimatorManager : AnimatorManager
{
    public AiAnimatorManager(Animator animator) : base(animator)
    {
    }


    public void Walkforward(bool b)
    {
        _animator.SetBool("WalkForward", b);
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



    public bool IsStunPlaying()
    {
        return IsAnimationPlayingWithName("Stun") || IsAnimationPlayingWithName("Stun 0") || IsTransitionPlayingWithName("ToStun");
    }
}
