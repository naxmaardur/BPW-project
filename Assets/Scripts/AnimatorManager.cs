using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager 
{
    protected Animator _animator;

    public AnimatorManager(Animator animator)
    {
        _animator = animator;
    }


    protected float _targetX;
    protected float _targetY;


    public Vector3 GetDeltaPosition { get { return _animator.deltaPosition; } }

    public bool IsAnimationPlaying()
    {
        return (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f && !_animator.IsInTransition(0);
    }

    public float GetAnimationCompletionPecentage()
    {
        return (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1;
    }

    public bool IsAnimationPlayingWithName(string name)
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    public bool IsTransitionPlayingWithName(string name)
    {
        AnimatorTransitionInfo transitionInfo = _animator.GetAnimatorTransitionInfo(0);
        return transitionInfo.IsUserName(name);
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
}
