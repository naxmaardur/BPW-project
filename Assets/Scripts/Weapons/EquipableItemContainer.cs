using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableItemContainer
{
    [SerializeField]
    GameObject[] _weapons;

    [SerializeField]
    MagicSpell[] _spells;

    public EquipableItemContainer()
    {
        _weapons = new GameObject[]
        {
            (GameObject)Resources.Load("Prefabs/Weapons/Greatsword", typeof(GameObject)),
            (GameObject)Resources.Load("Prefabs/Weapons/Sword", typeof(GameObject)),
            (GameObject)Resources.Load("Prefabs/Weapons/Axe", typeof(GameObject))
        };
        

        _spells = new MagicSpell[] 
        { 
           new SpawnEffectSpell("Prefabs/spells/IceLanceEffect",Color.blue), 
           new SpawnEffectSpell("Prefabs/spells/EarthShatterEffect", Color.green) 
        };
    }

    public GameObject GetWeaponById(int id)
    {
        return _weapons[id];
    }

    public MagicSpell GetSpellById(int id)
    {
        return _spells[id];
    }
}
