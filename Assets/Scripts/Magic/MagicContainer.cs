using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicContainer : MonoBehaviour
{
    MagicSpell _spell;
    GameObject _owner;
    Transform _castingPosition;
    [SerializeField]
    MeshRenderer _meshRenderer;
    int _spellCharges;
    Color _color;

    public GameObject Owner { get { return _owner; } set { if (_owner == null) { _owner = value; } } }
    public Transform CastingPosition { get { return _castingPosition; } set { if (_castingPosition == null) { _castingPosition = value; } } }
    public MagicSpell Spell { get { return _spell; } set { if (_spell == null) { _spell = value; } } }
    
    public Color Color { get { return _color; } set { _color = value; _meshRenderer.material.color = value; } }

    public int SpellCharges { get { return _spellCharges; } }

    private void Awake()
    {
        Color = Color.gray;
    }

    public void OnCast()
    {
        if(Owner == null) { return; }
        if(_spell == null) { return; }
        _spellCharges--;
        _spell.Cast(Owner,_castingPosition);
        if (_spellCharges <= 0) { RemoveSpell();}
    }

    public void RemoveSpell()
    {
        _spell = null;
        Color = Color.gray;
    }

    public void SetGemCollor(Color color)
    {
        _meshRenderer.material.color = color;
    }
    

    public void SetSpellCharges(int value)
    {
        _spellCharges = value;
    }

    public void EnableContainer()
    {
        _meshRenderer.enabled = true;
    }

    public void DisableContainer()
    {
        _meshRenderer.enabled = false;
    }


}
