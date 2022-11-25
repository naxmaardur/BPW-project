using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RunTimeAnimatorListContainer
{
    [SerializeField]
    private RuntimeAnimatorController[] _animators; 

    public RuntimeAnimatorController GetAnimator(int pos)
    {
        if (_animators.Length - 1 < pos) { return null; }
        return _animators[pos];
    }

}
