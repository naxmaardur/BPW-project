using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipableItemContainer
{
    [SerializeField]
    GameObject[] weapons;

    [SerializeField]
    GameObject[] Spells;

    public GameObject GetWeaponById(int id)
    {
        return weapons[id];
    }

    public GameObject GetSpellById(int id)
    {
        return Spells[id];
    }
}
