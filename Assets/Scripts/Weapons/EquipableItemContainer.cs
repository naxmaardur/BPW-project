using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipableItemContainer
{
    [SerializeField]
    GameObject[] weapons;

    [SerializeField]
    MagicSpell[] Spells;

    public void OnAwake()
    {
       Spells = new MagicSpell[] 
       { 
           new SpawnEffectSpell("Prefabs/spells/IceLanceEffect",Color.blue), 
           new SpawnEffectSpell("Prefabs/spells/EarthShatterEffect", Color.green) 
       };
    }

    public GameObject GetWeaponById(int id)
    {
        return weapons[id];
    }

    public MagicSpell GetSpellById(int id)
    {
        return Spells[id];
    }
}
