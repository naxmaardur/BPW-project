using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicContainer : MonoBehaviour
{
    [SerializeField]
    MagicSpell _spell;
    GameObject _owner;

    public GameObject Owner { get { return _owner; } set {if(_owner == null) { _owner = value; } } }


    public void OnCast()
    {
        if(Owner == null) { return; }
        _spell.Cast(Owner);
    }

}
